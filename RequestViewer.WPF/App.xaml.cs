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
        private readonly RequestsStore _requestsStore;

        public App()
        {
            _requestsStore = new RequestsStore(null, null, null, null);
            _selectedRequestStore = new SelectedRequestStore(_requestsStore);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow();
            MainWindow.DataContext = new MainViewModel(_selectedRequestStore, _requestsStore);
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
