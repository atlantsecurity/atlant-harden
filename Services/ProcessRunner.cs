using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AtlantHarden.Services
{
    /// <summary>Exit code and captured output of a child process.</summary>
    public sealed record ProcessResult(int ExitCode, string StdOut, string StdErr, bool TimedOut)
    {
        public bool Success => !TimedOut && ExitCode == 0;
    }

    /// <summary>
    /// The single place this (elevated) app starts helper processes.
    ///
    /// - Tools are launched by FULL PATH under %SystemRoot%. With UseShellExecute=false Windows
    ///   searches the application's own folder first, so launching "powershell.exe" by bare name
    ///   would run a same-named file planted next to AtlantHarden.exe (e.g. in Downloads) as admin.
    /// - stdout and stderr are always drained asynchronously, so a chatty child can't fill the
    ///   pipe buffer and hang the app forever.
    /// - PowerShell scripts are passed with -EncodedCommand, so quotes in a script can't break the
    ///   command line.
    /// - cmd.exe commands run as cmd /d /s /c "...", which keeps inner quotes intact even when a
    ///   quoted path contains parentheses (C:\Program Files (x86)\...).
    /// </summary>
    public static class ProcessRunner
    {
        public static string System32 => Environment.SystemDirectory;
        public static string PowerShellPath => Path.Combine(System32, @"WindowsPowerShell\v1.0\powershell.exe");
        public static string CmdPath => Path.Combine(System32, "cmd.exe");
        public static string Tool(string exeName) => Path.Combine(System32, exeName);

        public static Task<ProcessResult> RunPowerShellAsync(string script, TimeSpan? timeout = null)
        {
            var encoded = Convert.ToBase64String(Encoding.Unicode.GetBytes(script));
            return RunAsync(PowerShellPath,
                $"-NoProfile -NonInteractive -ExecutionPolicy Bypass -EncodedCommand {encoded}",
                timeout, logLabel: "PS> " + script);
        }

        public static Task<ProcessResult> RunCmdAsync(string command, TimeSpan? timeout = null)
            => RunAsync(CmdPath, $"/d /s /c \"{command}\"", timeout, logLabel: "CMD> " + command);

        public static async Task<ProcessResult> RunAsync(string fileName, string arguments,
            TimeSpan? timeout = null, string? logLabel = null)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WorkingDirectory = System32
                };

                using var process = Process.Start(psi);
                if (process == null)
                    return Log(logLabel ?? fileName, new ProcessResult(-1, "", "Process could not be started", false));

                var stdout = process.StandardOutput.ReadToEndAsync();
                var stderr = process.StandardError.ReadToEndAsync();

                bool timedOut = false;
                if (timeout is { } t)
                {
                    using var cts = new CancellationTokenSource(t);
                    try { await process.WaitForExitAsync(cts.Token); }
                    catch (OperationCanceledException)
                    {
                        timedOut = true;
                        try { process.Kill(entireProcessTree: true); } catch { }
                    }
                }
                else
                {
                    await process.WaitForExitAsync();
                }

                string so = "", se = "";
                try { so = await stdout; se = await stderr; } catch { }
                var code = timedOut ? -1 : process.ExitCode;
                return Log(logLabel ?? fileName, new ProcessResult(code, so, se, timedOut));
            }
            catch (Exception ex)
            {
                return Log(logLabel ?? fileName, new ProcessResult(-1, "", ex.Message, false));
            }
        }

        /// <summary>Synchronous variant for quick status queries (bounded by a timeout).</summary>
        public static ProcessResult Run(string fileName, string arguments, TimeSpan timeout)
            // Task.Run: no captured UI SynchronizationContext, so blocking here can't deadlock.
            => Task.Run(() => RunAsync(fileName, arguments, timeout)).GetAwaiter().GetResult();

        private static ProcessResult Log(string label, ProcessResult r)
        {
            // Only log state-changing commands (labelled) and failures; status queries are noisy.
            if (label.StartsWith("PS> ") || label.StartsWith("CMD> ") || !r.Success)
                ActivityLog.Write($"{Trim(label)} -> exit {r.ExitCode}{(r.TimedOut ? " (timed out)" : "")}"
                    + (string.IsNullOrWhiteSpace(r.StdErr) ? "" : $" | stderr: {Trim(r.StdErr)}"));
            return r;
        }

        private static string Trim(string s)
        {
            s = s.Replace("\r", " ").Replace("\n", " ").Trim();
            return s.Length > 400 ? s[..400] + "…" : s;
        }
    }

    /// <summary>
    /// Rolling plain-text log of every change command, its exit code and error output, at
    /// %LOCALAPPDATA%\AtlantHarden\Logs\activity.log. Used for support and by silent mode.
    /// </summary>
    public static class ActivityLog
    {
        private static readonly object Gate = new();
        public static string LogDirectory { get; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AtlantHarden", "Logs");
        public static string LogPath => Path.Combine(LogDirectory, "activity.log");

        public static void Write(string message)
        {
            try
            {
                lock (Gate)
                {
                    Directory.CreateDirectory(LogDirectory);
                    var fi = new FileInfo(LogPath);
                    if (fi.Exists && fi.Length > 5 * 1024 * 1024)
                    {
                        var old = Path.Combine(LogDirectory, "activity.old.log");
                        File.Delete(old);
                        File.Move(LogPath, old);
                    }
                    File.AppendAllText(LogPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}  {message}{Environment.NewLine}");
                }
            }
            catch { /* logging must never break the app */ }
        }
    }
}
