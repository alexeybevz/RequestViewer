using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Commands
{
    public class OpenEditRequestCommand : CommandBase
    {
        private RequestsListingItemViewModel _requestsListingItemViewModel;
        private RequestsStore _requestsStore;
        private ModalNavigationStore _modalNavigationStore;

        public OpenEditRequestCommand(RequestsListingItemViewModel requestsListingItemViewModel, RequestsStore requestsStore, ModalNavigationStore modalNavigationStore)
        {
            _requestsListingItemViewModel = requestsListingItemViewModel;
            _requestsStore = requestsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object? parameter)
        {
            var editYouTubeViewerViewModel = new EditRequestViewModel();
            _modalNavigationStore.CurrentViewModel = editYouTubeViewerViewModel;
        }
    }
}
