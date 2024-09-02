using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly SelectedRequestStore _selectedRequestStore;
        private RequestsStore _requestsStore;

        public RequestViewerViewModel RequestViewerViewModel { get; }

        public MainViewModel(SelectedRequestStore selectedRequestStore, RequestsStore requestsStore)
        {
            _selectedRequestStore = selectedRequestStore;
            _requestsStore = requestsStore;

            RequestViewerViewModel = RequestViewerViewModel.LoadViewModel(requestsStore, selectedRequestStore);
        }
    }
}
