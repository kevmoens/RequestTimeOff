using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RequestTimeOff.UserControls
{
    /// <summary>
    /// Interaction logic for MinMaxClose.xaml
    /// </summary>
    public partial class MinMaxClose : UserControl
    {
        public MinMaxClose()
        {
            InitializeComponent();
        }
        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(Window.GetWindow(this));
        }

        private void CommandBinding_Executed_2(object sender, ExecutedRoutedEventArgs e)
        {
            if (Window.GetWindow(this).WindowState == WindowState.Maximized)
            {
                SystemCommands.RestoreWindow(Window.GetWindow(this));
                return;
            }
            SystemCommands.MaximizeWindow(Window.GetWindow(this));
        }

        private void CommandBinding_Executed_3(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(Window.GetWindow(this));
        }
    }
}
