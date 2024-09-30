using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Commands
{
    public class OpenEditRequestCommand : CommandBase
    {
        private readonly RequestsListingItemViewModel _requestsListingItemViewModel;
        private readonly RequestsStore _requestsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenEditRequestCommand(RequestsListingItemViewModel requestsListingItemViewModel, RequestsStore requestsStore, ModalNavigationStore modalNavigationStore)
        {
            _requestsListingItemViewModel = requestsListingItemViewModel;
            _requestsStore = requestsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object? parameter)
        {
            var vm = new EditRequestViewModel(_requestsListingItemViewModel.Request, _requestsStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = vm;
        }
    }
}
