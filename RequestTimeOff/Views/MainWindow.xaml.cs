using RequestTimeOff.ViewModels;
using System.Windows.Input;
using System.Windows.Navigation;

namespace RequestTimeOff.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow(MainWindowViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public MainWindow()
        {
            InitializeComponent();
            MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
        }

        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }
    }
}
