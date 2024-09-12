using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Commands
{
    public class OpenEditRequestCommand : CommandBase
    {
        private RequestsListingItemViewModel _requestsListingItemViewModel;
        private RequestsStore _requestsStore;
        private ModalNavigationStore _modalNavigationStore;
        private readonly SelectedRequestStore _selectedRequestStore;

        public OpenEditRequestCommand(RequestsListingItemViewModel requestsListingItemViewModel, RequestsStore requestsStore, ModalNavigationStore modalNavigationStore, SelectedRequestStore selectedRequestStore)
        {
            _requestsListingItemViewModel = requestsListingItemViewModel;
            _requestsStore = requestsStore;
            _modalNavigationStore = modalNavigationStore;
            _selectedRequestStore = selectedRequestStore;
        }

        public override void Execute(object? parameter)
        {
            var vm = new EditRequestViewModel(_selectedRequestStore.SelectedRequest, _requestsStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = vm;
        }
    }
}
