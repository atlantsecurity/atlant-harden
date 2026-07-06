using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AtlantHarden.Views
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && !string.IsNullOrEmpty(value.ToString()) 
                ? Visibility.Visible 
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool b && !b;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool b && !b;
        }
    }

    public class BoolToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool b && b ? 1.0 : 0.5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringToTagConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string currentView && parameter is string expectedView)
            {
                return currentView == expectedView ? "Selected" : null!;
            }
            return null!;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FileSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long bytes)
            {
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                int order = 0;
                double size = bytes;
                
                while (size >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    size /= 1024;
                }
                
                return $"{size:0.##} {sizes[order]}";
            }
            return "0 B";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dt)
            {
                var now = DateTime.Now;
                var diff = now - dt;

                if (diff.TotalMinutes < 1)
                    return "Just now";
                if (diff.TotalHours < 1)
                    return $"{(int)diff.TotalMinutes} minutes ago";
                if (diff.TotalDays < 1)
                    return $"{(int)diff.TotalHours} hours ago";
                if (diff.TotalDays < 7)
                    return $"{(int)diff.TotalDays} days ago";
                
                return dt.ToString("MMM d, yyyy h:mm tt");
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RiskLevelToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Models.RiskLevel risk)
            {
                return risk switch
                {
                    Models.RiskLevel.Low => Application.Current.FindResource("SuccessBrush"),
                    Models.RiskLevel.Medium => Application.Current.FindResource("WarningBrush"),
                    Models.RiskLevel.High => Application.Current.FindResource("DangerBrush"),
                    _ => Application.Current.FindResource("TextSecondaryBrush")
                };
            }
            return Application.Current.FindResource("TextSecondaryBrush");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Models.BackupStatus status)
            {
                return status == Models.BackupStatus.Corrupted ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StatusToEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Models.BackupStatus status)
            {
                return status != Models.BackupStatus.Corrupted;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                bool invert = parameter?.ToString() == "Invert";
                bool hasItems = count > 0;
                
                if (invert)
                    return hasItems ? Visibility.Collapsed : Visibility.Visible;
                else
                    return hasItems ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ScoreToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int score = 0;
            if (value is int intVal)
                score = intVal;
            else if (value is double doubleVal)
                score = (int)doubleVal;
            else if (value is string strVal && int.TryParse(strVal, out int parsed))
                score = parsed;

            // Red below 40, Orange 40-70, Green 70+
            if (score < 40)
                return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(217, 48, 37)); // #D93025 Red
            else if (score < 70)
                return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 152, 0)); // #FF9800 Orange
            else
                return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(76, 175, 80)); // #4CAF50 Green
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
