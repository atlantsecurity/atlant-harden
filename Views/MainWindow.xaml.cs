using AtlantHarden.Models;
using AtlantHarden.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AtlantHarden.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is CategoryInfo category)
            {
                if (DataContext is MainViewModel vm)
                {
                    vm.SelectedCategory = category;
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Keyboard shortcuts
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.A:
                        // Select All
                        if (DataContext is MainViewModel vm1)
                        {
                            if (vm1.SelectAllCommand.CanExecute(null)) vm1.SelectAllCommand.Execute(null);
                        }
                        e.Handled = true;
                        break;

                    case Key.D:
                        // Deselect All
                        if (DataContext is MainViewModel vm2)
                        {
                            if (vm2.DeselectAllCommand.CanExecute(null)) vm2.DeselectAllCommand.Execute(null);
                        }
                        e.Handled = true;
                        break;

                    case Key.R:
                        // Refresh
                        if (DataContext is MainViewModel vm3)
                        {
                            if (vm3.RefreshStatusCommand.CanExecute(null)) vm3.RefreshStatusCommand.Execute(null);
                        }
                        e.Handled = true;
                        break;

                    case Key.S:
                        // Apply Selected
                        if (DataContext is MainViewModel vm4)
                        {
                            if (vm4.ApplySelectedCommand.CanExecute(null)) vm4.ApplySelectedCommand.Execute(null);
                        }
                        e.Handled = true;
                        break;
                }
            }
        }
    }
}
