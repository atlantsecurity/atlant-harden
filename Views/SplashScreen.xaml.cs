using System;
using System.Windows;

namespace AtlantHarden.Views
{
    public partial class SplashScreen : Window
    {
        private const double MaxProgressWidth = 390; // Total available width for progress bar

        public SplashScreen()
        {
            InitializeComponent();
        }

        public void UpdateProgress(int current, int total, string message)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => UpdateProgress(current, total, message));
                return;
            }

            StatusText.Text = message;
            
            // Detect phase from message
            if (message.Contains("ASR") || message.Contains("Attack Surface"))
            {
                PhaseText.Text = "🎯 Phase 1: Attack Surface Reduction Rules";
            }
            else if (message.Contains("registry") || message.Contains("Registry"))
            {
                PhaseText.Text = "🔑 Phase 2: Registry Settings";
            }
            else if (message.Contains("Verification") || message.Contains("complete"))
            {
                PhaseText.Text = "✓ Verification Complete";
            }
            
            double percentage = total > 0 ? (double)current / total * 100 : 0;
            ProgressText.Text = $"{percentage:F0}%";
            
            // Animate progress bar width
            double targetWidth = (percentage / 100.0) * MaxProgressWidth;
            ProgressFill.Width = Math.Max(0, Math.Min(targetWidth, MaxProgressWidth));
        }

        public void SetStatus(string message)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => SetStatus(message));
                return;
            }

            StatusText.Text = message;
        }

        public void SetPhase(string phase)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => SetPhase(phase));
                return;
            }

            PhaseText.Text = phase;
        }

        public void Complete()
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(Complete);
                return;
            }

            PhaseText.Text = "✓ Ready";
            StatusText.Text = "Launching application...";
            ProgressText.Text = "100%";
            ProgressFill.Width = MaxProgressWidth;
        }
    }
}
