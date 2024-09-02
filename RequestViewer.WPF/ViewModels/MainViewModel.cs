using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly SelectedRequestStore _selectedRequestStore;

        public RequestViewerViewModel RequestViewerViewModel { get; }

        public MainViewModel(SelectedRequestStore selectedRequestStore)
        {
            _selectedRequestStore = selectedRequestStore;

            RequestViewerViewModel = new RequestViewerViewModel(_selectedRequestStore);
        }
    }
}
