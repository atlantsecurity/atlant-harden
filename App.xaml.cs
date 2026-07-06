using System;
using System.Linq;
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

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

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

            // Parse command line arguments
            ParseCommandLine(e.Args);

            // Handle --help
            if (e.Args.Contains("--help") || e.Args.Contains("-h") || e.Args.Contains("/?"))
            {
                ShowHelp();
                Shutdown(0);
                return;
            }

            // Check if running as administrator
            if (!IsRunningAsAdministrator())
            {
                var message = "This application requires administrator privileges to modify system security settings.\n\n" +
                    "Please right-click the application and select 'Run as administrator'.";
                
                if (SilentMode)
                {
                    Console.Error.WriteLine("ERROR: " + message);
                    Shutdown(1);
                    return;
                }
                
                MessageBox.Show(message, "Administrator Privileges Required",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                
                Shutdown(1);
                return;
            }

            // Validate config file if provided
            if (!string.IsNullOrEmpty(ConfigFilePath))
            {
                var (isValid, validationMessage, _) = ConfigurationService.ValidateConfiguration(ConfigFilePath);
                if (!isValid)
                {
                    var errorMsg = $"Invalid configuration file: {validationMessage}";
                    if (SilentMode)
                    {
                        Console.Error.WriteLine("ERROR: " + errorMsg);
                        Shutdown(1);
                        return;
                    }
                    
                    MessageBox.Show(errorMsg, "Configuration Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    Shutdown(1);
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
                    if (!SilentMode) _splashScreen?.SetStatus("Applying configuration...");
                    var profile = ConfigurationService.LoadConfiguration(ConfigFilePath);
                    if (profile != null)
                    {
                        viewModel.ImportConfiguration(profile);
                        
                        // If auto-apply and silent mode, apply settings and exit
                        if (AutoApply && SilentMode)
                        {
                            Console.WriteLine($"Applying configuration: {profile.Name}");
                            Console.WriteLine($"Settings to apply: {profile.EnabledCount}");
                            
                            await viewModel.ApplyAllEnabledAsync();
                            
                            Console.WriteLine("Configuration applied successfully.");
                            Shutdown(0);
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
                }
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error initializing application: {ex.Message}";
                if (SilentMode)
                {
                    Console.Error.WriteLine("ERROR: " + errorMsg);
                    Shutdown(1);
                    return;
                }
                
                MessageBox.Show(errorMsg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        {
                            ConfigFilePath = args[++i];
                        }
                        break;
                        
                    case "--apply":
                    case "-a":
                        AutoApply = true;
                        break;
                        
                    case "--silent":
                    case "-s":
                        SilentMode = true;
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

Configuration Files:
  Export configurations from the app using File > Export Configuration.
  Configuration files are JSON format with enabled setting IDs.

Note: Administrator privileges are required.
";
            MessageBox.Show(help.Trim(), "AtlantHarden - Help", 
                MessageBoxButton.OK, MessageBoxImage.Information);
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

