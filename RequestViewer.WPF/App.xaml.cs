using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;
using System.Windows;

namespace RequestViewer.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly SelectedRequestStore _selectedRequestStore;

        public App()
        {
            _selectedRequestStore = new SelectedRequestStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow();
            MainWindow.DataContext = new MainViewModel(_selectedRequestStore);
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
