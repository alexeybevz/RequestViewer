using Microsoft.EntityFrameworkCore;
using RequestViewer.Domain.Queries;
using RequestViewer.EntityFramework;
using RequestViewer.EntityFramework.Queries;
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
        private readonly RequestViewerDbContextFactory _requestViewerDbContextFactory;

        private readonly IGetAllRequestsQuery _getAllRequestsQuery;

        public App()
        {
            string connectionString = "Data Source=RequestViewer.db";

            _requestViewerDbContextFactory = new RequestViewerDbContextFactory(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);

            _getAllRequestsQuery = new GetAllRequestsQuery(_requestViewerDbContextFactory, new GetAllUsersQuery(_requestViewerDbContextFactory));

            _requestsStore = new RequestsStore(_getAllRequestsQuery, null, null, null);
            _selectedRequestStore = new SelectedRequestStore(_requestsStore);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (RequestViewerDbContext context = _requestViewerDbContextFactory.Create())
            {
                context.Database.Migrate();
            }

            MainWindow = new MainWindow();
            MainWindow.DataContext = new MainViewModel(_selectedRequestStore, _requestsStore);
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
