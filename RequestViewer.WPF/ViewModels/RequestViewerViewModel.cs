using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestViewerViewModel : ViewModelBase
    {
        private readonly SelectedRequestStore _selectedRequestStore;

        public RequestsListingViewModel RequestsListingViewModel { get; }
        public RequestsDetailsViewModel RequestsDetailsViewModel { get; }

        public RequestViewerViewModel(SelectedRequestStore selectedRequestStore)
        {
            _selectedRequestStore = selectedRequestStore;

            RequestsListingViewModel = new RequestsListingViewModel(_selectedRequestStore);
            RequestsDetailsViewModel = new RequestsDetailsViewModel(_selectedRequestStore);
        }
    }
}
