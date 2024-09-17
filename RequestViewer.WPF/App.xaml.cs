using Microsoft.EntityFrameworkCore;
using RequestViewer.Domain.Commands;
using RequestViewer.Domain.Queries;
using RequestViewer.EntityFramework;
using RequestViewer.EntityFramework.Commands;
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
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly UsersStore _usersStore;
        private readonly PeriodsStore _periodsStore;
        private readonly RequestViewerDbContextFactory _requestViewerDbContextFactory;

        private readonly IGetAllRequestsQuery _getAllRequestsQuery;
        private readonly ICreateRequestCommand _createRequestCommand;
        private readonly IUpdateRequestCommand _updateRequestCommand;
        private readonly IDeleteRequestCommand _deleteRequestCommand;
        private readonly IApproveRequestCommand _approveRequestCommand;
        private readonly IRejectRequestCommand _rejectRequestCommand;

        public App()
        {
            string connectionString = "Data Source=RequestViewer.db";

            _requestViewerDbContextFactory = new RequestViewerDbContextFactory(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);

            _getAllRequestsQuery = new GetAllRequestsQuery(_requestViewerDbContextFactory, new GetAllUsersQuery(_requestViewerDbContextFactory));
            _createRequestCommand = new CreateRequestCommand(_requestViewerDbContextFactory);
            _updateRequestCommand = new UpdateRequestCommand(_requestViewerDbContextFactory);
            _deleteRequestCommand = new DeleteRequestCommand(_requestViewerDbContextFactory);
            _approveRequestCommand = new ApproveRequestCommand(_requestViewerDbContextFactory);
            _rejectRequestCommand = new RejectRequestCommand(_requestViewerDbContextFactory);

            _requestsStore = new RequestsStore(
                _getAllRequestsQuery,
                _createRequestCommand,
                _updateRequestCommand,
                _deleteRequestCommand,
                _approveRequestCommand,
                _rejectRequestCommand);
            _selectedRequestStore = new SelectedRequestStore(_requestsStore);
            _modalNavigationStore = new ModalNavigationStore();
            _usersStore = new UsersStore(new GetAllUsersQuery(_requestViewerDbContextFactory));
            _periodsStore = new PeriodsStore(new GetAllPeriodsQuery(_requestViewerDbContextFactory));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (RequestViewerDbContext context = _requestViewerDbContextFactory.Create())
            {
                context.Database.Migrate();
            }

            MainWindow = new MainWindow();
            MainWindow.DataContext = new MainViewModel(_selectedRequestStore, _requestsStore, _modalNavigationStore, _usersStore, _periodsStore);
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
