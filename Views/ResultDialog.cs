using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AtlantHarden.Views
{
    /// <summary>
    /// Scrollable, copyable result/review dialog used instead of long MessageBoxes: lists every
    /// item (not just the first 10), can copy or save the text, and can offer a restart.
    /// </summary>
    public sealed class ResultDialog : Window
    {
        public bool Confirmed { get; private set; }

        private ResultDialog(string title, string summary, string details, string okText, string? cancelText,
            bool offerRestart, string? logFolder)
        {
            Title = title;
            Width = 720; Height = 560; MinWidth = 480; MinHeight = 320;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Background = new SolidColorBrush(Color.FromRgb(0x0D, 0x11, 0x17));
            Foreground = new SolidColorBrush(Color.FromRgb(0xF0, 0xF6, 0xFC));
            Owner = Application.Current?.MainWindow is { IsVisible: true } mw ? mw : null;
            SourceInitialized += (_, _) => MainWindow.UseDarkTitleBar(this);

            var root = new Grid { Margin = new Thickness(20) };
            root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var head = new TextBlock
            {
                Text = summary, TextWrapping = TextWrapping.Wrap, FontSize = 14,
                Margin = new Thickness(0, 0, 0, 12), Foreground = Foreground
            };
            Grid.SetRow(head, 0);
            root.Children.Add(head);

            var box = new TextBox
            {
                Text = details, IsReadOnly = true, TextWrapping = TextWrapping.Wrap,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                FontFamily = new FontFamily("Consolas"), FontSize = 12,
                Background = new SolidColorBrush(Color.FromRgb(0x16, 0x1B, 0x22)),
                Foreground = new SolidColorBrush(Color.FromRgb(0xC9, 0xD1, 0xD9)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(0x30, 0x36, 0x3D)),
                Padding = new Thickness(8)
            };
            System.Windows.Automation.AutomationProperties.SetName(box, "Details");
            Grid.SetRow(box, 1);
            root.Children.Add(box);

            var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 12, 0, 0) };
            Button Btn(string text, Action onClick, bool primary = false)
            {
                var b = new Button { Content = text, Padding = new Thickness(14, 6, 14, 6), Margin = new Thickness(8, 0, 0, 0), MinWidth = 90, IsDefault = primary };
                b.Click += (_, _) => onClick();
                buttons.Children.Add(b);
                return b;
            }

            Btn("Copy", () => { try { Clipboard.SetText(summary + Environment.NewLine + Environment.NewLine + details); } catch { } });
            Btn("Save…", () =>
            {
                var dlg = new Microsoft.Win32.SaveFileDialog { Filter = "Text file (*.txt)|*.txt", FileName = $"AtlantHarden_{DateTime.Now:yyyyMMdd_HHmmss}.txt" };
                if (dlg.ShowDialog(this) == true) File.WriteAllText(dlg.FileName, summary + Environment.NewLine + Environment.NewLine + details);
            });
            if (logFolder != null)
                Btn("Open log", () =>
                {
                    try { Process.Start(new ProcessStartInfo { FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "explorer.exe"), Arguments = $"\"{logFolder}\"", UseShellExecute = false }); } catch { }
                });
            if (offerRestart)
                Btn("Restart now", () =>
                {
                    if (MessageBox.Show(this, "Restart Windows now? Save your work in other apps first.", "Restart",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try { Process.Start(new ProcessStartInfo { FileName = Path.Combine(Environment.SystemDirectory, "shutdown.exe"), Arguments = "/r /t 5", UseShellExecute = false, CreateNoWindow = true }); } catch { }
                        Close();
                    }
                });
            if (cancelText != null)
                Btn(cancelText, () => { Confirmed = false; Close(); }).IsCancel = true;
            Btn(okText, () => { Confirmed = true; Close(); }, primary: true);

            Grid.SetRow(buttons, 2);
            root.Children.Add(buttons);
            Content = root;
        }

        /// <summary>Show a result; returns when closed.</summary>
        public static void ShowResult(string title, string summary, string details, bool offerRestart = false, string? logFolder = null)
            => new ResultDialog(title, summary, details, "Close", null, offerRestart, logFolder).ShowDialog();

        /// <summary>Show a review list and ask to continue. Returns true on confirm.</summary>
        public static bool Confirm(string title, string summary, string details, string okText = "Apply")
        {
            var d = new ResultDialog(title, summary, details, okText, "Cancel", false, null);
            d.ShowDialog();
            return d.Confirmed;
        }
    }
}
