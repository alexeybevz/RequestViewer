using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Stores;
using System.Windows.Input;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestViewerViewModel : ViewModelBase
    {
        private readonly SelectedRequestStore _selectedRequestStore;

        public RequestsListingViewModel RequestsListingViewModel { get; }
        public RequestsDetailsViewModel RequestsDetailsViewModel { get; }

        public RequestViewerViewModel(RequestsStore requestsStore, SelectedRequestStore selectedRequestStore)
        {
            _selectedRequestStore = selectedRequestStore;

            RequestsListingViewModel = new RequestsListingViewModel(_selectedRequestStore, requestsStore);
            RequestsDetailsViewModel = new RequestsDetailsViewModel(_selectedRequestStore, requestsStore);

            LoadRequestsCommand = new LoadRequestsCommand(this, requestsStore);
        }

        public ICommand LoadRequestsCommand { get; set; }

        public static RequestViewerViewModel LoadViewModel(RequestsStore requestsStore, SelectedRequestStore selectedRequestStore)
        {
            var vm = new RequestViewerViewModel(requestsStore, selectedRequestStore);
            vm.LoadRequestsCommand.Execute(null);
            return vm;
        }
    }
}
