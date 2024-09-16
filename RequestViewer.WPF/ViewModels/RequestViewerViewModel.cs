using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Stores;
using System.Windows.Input;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestViewerViewModel : ViewModelBase
    {
        public RequestsListingViewModel RequestsListingViewModel { get; }
        public RequestsDetailsViewModel RequestsDetailsViewModel { get; }

        public RequestViewerViewModel(RequestsStore requestsStore, SelectedRequestStore selectedRequestStore, ModalNavigationStore modalNavigationStore, UsersStore usersStore)
        {
            RequestsListingViewModel = new RequestsListingViewModel(selectedRequestStore, requestsStore, modalNavigationStore);
            RequestsDetailsViewModel = new RequestsDetailsViewModel(selectedRequestStore, requestsStore);

            LoadRequestsCommand = new LoadRequestsCommand(this, requestsStore);
            OpenAddRequestCommand = new OpenAddRequestCommand(usersStore, modalNavigationStore);
        }

        public ICommand LoadRequestsCommand { get; set; }
        public ICommand OpenAddRequestCommand { get; }

        public static RequestViewerViewModel LoadViewModel(RequestsStore requestsStore, SelectedRequestStore selectedRequestStore, ModalNavigationStore modalNavigationStore, UsersStore usersStore)
        {
            var vm = new RequestViewerViewModel(requestsStore, selectedRequestStore, modalNavigationStore, usersStore);
            vm.LoadRequestsCommand.Execute(null);
            return vm;
        }
    }
}
