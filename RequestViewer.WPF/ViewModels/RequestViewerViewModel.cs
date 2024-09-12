using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Stores;
using System.Windows.Input;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestViewerViewModel : ViewModelBase
    {
        public RequestsListingViewModel RequestsListingViewModel { get; }
        public RequestsDetailsViewModel RequestsDetailsViewModel { get; }

        public RequestViewerViewModel(RequestsStore requestsStore, SelectedRequestStore selectedRequestStore, ModalNavigationStore modalNavigationStore)
        {
            RequestsListingViewModel = new RequestsListingViewModel(selectedRequestStore, requestsStore, modalNavigationStore);
            RequestsDetailsViewModel = new RequestsDetailsViewModel(selectedRequestStore, requestsStore);

            LoadRequestsCommand = new LoadRequestsCommand(this, requestsStore);
        }

        public ICommand LoadRequestsCommand { get; set; }

        public static RequestViewerViewModel LoadViewModel(RequestsStore requestsStore, SelectedRequestStore selectedRequestStore, ModalNavigationStore modalNavigationStore)
        {
            var vm = new RequestViewerViewModel(requestsStore, selectedRequestStore, modalNavigationStore);
            vm.LoadRequestsCommand.Execute(null);
            return vm;
        }
    }
}
