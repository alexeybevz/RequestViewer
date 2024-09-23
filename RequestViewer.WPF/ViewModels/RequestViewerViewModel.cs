using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Stores;
using System.Windows.Input;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestViewerViewModel : ViewModelBase
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public RequestsListingViewModel RequestsListingViewModel { get; }
        public RequestsDetailsViewModel RequestsDetailsViewModel { get; }

        public RequestViewerViewModel(RequestsStore requestsStore, SelectedRequestStore selectedRequestStore, ModalNavigationStore modalNavigationStore, UsersStore usersStore, PeriodsStore periodsStore)
        {
            LoadRequestsCommand = new LoadRequestsCommand(this, requestsStore);
            OpenAddRequestCommand = new OpenAddRequestCommand(requestsStore, usersStore, modalNavigationStore, periodsStore);

            RequestsListingViewModel = new RequestsListingViewModel(this, selectedRequestStore, requestsStore, modalNavigationStore);
            RequestsDetailsViewModel = new RequestsDetailsViewModel(selectedRequestStore, requestsStore);
        }

        public ICommand LoadRequestsCommand { get; set; }
        public ICommand OpenAddRequestCommand { get; }

        public static RequestViewerViewModel LoadViewModel(RequestsStore requestsStore, SelectedRequestStore selectedRequestStore, ModalNavigationStore modalNavigationStore, UsersStore usersStore, PeriodsStore periodsStore)
        {
            var vm = new RequestViewerViewModel(requestsStore, selectedRequestStore, modalNavigationStore, usersStore, periodsStore);
            vm.LoadRequestsCommand.Execute(null);
            return vm;
        }
    }
}
