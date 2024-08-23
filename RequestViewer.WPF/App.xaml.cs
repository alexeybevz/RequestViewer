using RequestViewer.WPF.ViewModels;
using System.Windows;

namespace RequestViewer.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow();
            MainWindow.DataContext = new MainViewModel();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
