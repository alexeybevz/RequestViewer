namespace RequestViewer.WPF.ViewModels
{
    public class RequestViewerViewModel : ViewModelBase
    {
        public RequestsListingViewModel RequestsListingViewModel { get; }

        public RequestViewerViewModel()
        {
            RequestsListingViewModel = new RequestsListingViewModel();
        }
    }
}
