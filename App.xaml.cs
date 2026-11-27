using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;
using AtlantHarden.Services;
using AtlantHarden.ViewModels;

namespace AtlantHarden
{
    public partial class App : Application
    {
        private Views.SplashScreen? _splashScreen;
        
        /// <summary>
        /// Configuration file path passed via command line
        /// </summary>
        public static string? ConfigFilePath { get; private set; }
        
        /// <summary>
        /// Whether to auto-apply the configuration
        /// </summary>
        public static bool AutoApply { get; private set; }
        
        /// <summary>
        /// Whether running in silent/headless mode
        /// </summary>
        public static bool SilentMode { get; private set; }

        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);
        private const int AttachParentProcess = -1;

        // Exit codes for automation (--silent).
        public const int ExitOk = 0, ExitError = 1, ExitBadArgs = 2, ExitSomeFailed = 3;

        private static readonly List<string> ArgErrors = new();

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Parse first: silent mode changes how every message below is reported.
            ParseCommandLine(e.Args);
            if (SilentMode)
            {
                // WinExe has no console of its own; attach to the caller's so output is visible.
                AttachConsole(AttachParentProcess);
                ShutdownMode = ShutdownMode.OnExplicitShutdown;
            }

            // Global exception handlers
            DispatcherUnhandledException += (s, args) =>
            {
                MessageBox.Show(
                    $"An unexpected error occurred:\n\n{args.Exception.Message}\n\nThe application will attempt to continue.",
                    "AtlantHarden Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                args.Handled = true;
            };

            AppDomain.CurrentDomain.UnhandledException += (s, args) =>
            {
                if (args.ExceptionObject is Exception ex)
                {
                    MessageBox.Show(
                        $"A fatal error occurred:\n\n{ex.Message}",
                        "AtlantHarden Fatal Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            };

            TaskScheduler.UnobservedTaskException += (s, args) =>
            {
                args.SetObserved(); // Prevent crash from fire-and-forget tasks
            };

            // Handle --help
            if (e.Args.Any(a => a is "--help" or "-h" or "/?"))
            {
                ShowHelp();
                Shutdown(ExitOk);
                return;
            }

            if (ArgErrors.Count > 0 || (SilentMode && (string.IsNullOrEmpty(ConfigFilePath) || !AutoApply)))
            {
                if (SilentMode && (string.IsNullOrEmpty(ConfigFilePath) || !AutoApply))
                    ArgErrors.Add("--silent requires both --config <file> and --apply.");
                Report("ERROR: " + string.Join("\n", ArgErrors) + "\nRun AtlantHarden.exe --help for usage.", isError: true);
                Shutdown(ExitBadArgs);
                return;
            }

            // Check if running as administrator
            if (!IsRunningAsAdministrator())
            {
                var message = "This application requires administrator privileges to modify system security settings.\n\n" +
                    "Please right-click the application and select 'Run as administrator'.";
                
                Report("ERROR: " + message, isError: true, title: "Administrator Privileges Required");
                Shutdown(ExitError);
                return;
            }

            // Validate config file if provided
            if (!string.IsNullOrEmpty(ConfigFilePath))
            {
                var (isValid, validationMessage, _) = ConfigurationService.ValidateConfiguration(ConfigFilePath);
                if (!isValid)
                {
                    Report($"ERROR: Invalid configuration file: {validationMessage}", isError: true, title: "Configuration Error");
                    Shutdown(SilentMode ? ExitBadArgs : ExitError);
                    return;
                }
            }

            // Show splash screen (unless silent mode)
            if (!SilentMode)
            {
                _splashScreen = new Views.SplashScreen();
                _splashScreen.Show();
            }

            try
            {
                // Create the main view model and initialize it with progress updates
                if (!SilentMode) _splashScreen?.SetStatus("Loading settings...");
                var viewModel = new MainViewModel();

                // Initialize settings and check ASR status in background
                var progress = new Progress<(int current, int total, string message)>(update =>
                {
                    if (!SilentMode)
                        _splashScreen?.UpdateProgress(update.current, update.total, update.message);
                });

                await viewModel.InitializeAsync(progress);

                // Apply configuration if provided
                if (!string.IsNullOrEmpty(ConfigFilePath))
                {
                    if (!SilentMode) _splashScreen?.SetStatus("Loading configuration...");
                    var profile = ConfigurationService.LoadConfiguration(ConfigFilePath);
                    if (profile == null)
                    {
                        Report($"ERROR: Could not load configuration: {ConfigFilePath}", isError: true, title: "Configuration Error");
                        if (SilentMode) { Shutdown(ExitBadArgs); return; }
                    }
                    else
                    {
                        var unknown = viewModel.ImportConfiguration(profile);
                        if (unknown.Count > 0)
                            Report($"WARNING: {unknown.Count} setting id(s) in the configuration are not known to this version and were skipped: "
                                   + string.Join(", ", unknown.Take(10)) + (unknown.Count > 10 ? ", …" : ""), isError: false);

                        if (AutoApply && SilentMode)
                        {
                            Console.WriteLine($"AtlantHarden {ConfigurationService.CurrentAppVersion}: applying configuration '{profile.Name}'");
                            var result = await viewModel.ApplyAllEnabledAsync();
                            Console.WriteLine($"Applied: {result.Applied}  Already applied: {result.AlreadyApplied}  Failed: {result.Failed.Count}");
                            foreach (var f in result.Failed)
                                Console.Error.WriteLine($"FAILED: {f}");
                            Console.WriteLine($"Backup: {result.BackupPath}");
                            Console.WriteLine($"Log: {ActivityLog.LogPath}");
                            ActivityLog.Write($"Silent apply '{profile.Name}': applied {result.Applied}, failed {result.Failed.Count}");
                            Shutdown(result.Failed.Count == 0 ? ExitOk : ExitSomeFailed);
                            return;
                        }
                    }
                }

                if (!SilentMode)
                {
                    _splashScreen?.Complete();
                    await Task.Delay(300); // Brief pause to show completion

                    // Show main window
                    var mainWindow = new Views.MainWindow
                    {
                        DataContext = viewModel
                    };

                    MainWindow = mainWindow;
                    mainWindow.Show();
                    _ = viewModel.OfferFirstRunRestorePointAsync();
                }
            }
            catch (Exception ex)
            {
                Report($"ERROR: Error initializing application: {ex.Message}", isError: true, title: "Error");
                Shutdown(ExitError);
                return;
            }
            finally
            {
                // Close splash screen
                if (!SilentMode)
                {
                    _splashScreen?.Close();
                    _splashScreen = null;
                }
            }
        }

        private void ParseCommandLine(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i].ToLowerInvariant();
                
                switch (arg)
                {
                    case "--config":
                    case "-c":
                        if (i + 1 < args.Length)
                            ConfigFilePath = args[++i];
                        else
                            ArgErrors.Add("--config needs a file path.");
                        break;
                        
                    case "--apply":
                    case "-a":
                        AutoApply = true;
                        break;
                        
                    case "--silent":
                    case "-s":
                        SilentMode = true;
                        break;

                    case "--help":
                    case "-h":
                    case "/?":
                        break;

                    default:
                        ArgErrors.Add($"Unknown option: {args[i]}");
                        break;
                }
            }
        }

        private void ShowHelp()
        {
            var help = @"
AtlantHarden - Windows Hardening Tool
=========================================

Usage: AtlantHarden.exe [options]

Options:
  --config, -c <file>    Load configuration from JSON file
  --apply, -a            Auto-apply the loaded configuration
  --silent, -s           Run in silent mode (no GUI, for automation)
  --help, -h, /?         Show this help message

Examples:
  AtlantHarden.exe
      Launch the application normally with GUI

  AtlantHarden.exe --config myconfig.json
      Launch GUI with configuration pre-loaded

  AtlantHarden.exe --config myconfig.json --apply --silent
      Apply configuration silently and exit (for automation/scripts)

Exit codes (--silent):
  0  all settings applied        2  bad arguments or configuration
  1  error (e.g. not elevated)   3  some settings failed (see output / log)

Log: %ProgramFiles%\AtlantHarden\Logs\activity.log

Configuration Files:
  Export configurations from the app using File > Export Configuration.
  Configuration files are JSON format with enabled setting IDs.

Note: Administrator privileges are required.
";
            Report(help.Trim(), isError: false, title: "AtlantHarden - Help");
        }

        /// <summary>Console in silent mode (attached to the caller), a message box otherwise.</summary>
        private static void Report(string message, bool isError, string title = "AtlantHarden")
        {
            ActivityLog.Write(message);
            if (SilentMode)
            {
                if (isError) Console.Error.WriteLine(message); else Console.WriteLine(message);
                return;
            }
            MessageBox.Show(message, title, MessageBoxButton.OK,
                isError ? MessageBoxImage.Error : MessageBoxImage.Information);
        }

        private static bool IsRunningAsAdministrator()
        {
            try
            {
                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                return false;
            }
        }
    }
}

