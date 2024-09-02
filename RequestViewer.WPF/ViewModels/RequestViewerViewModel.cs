namespace RequestViewer.WPF.ViewModels
{
    public class RequestViewerViewModel : ViewModelBase
    {
        public RequestsListingViewModel RequestsListingViewModel { get; }
        public RequestsDetailsViewModel RequestsDetailsViewModel { get; }

        public RequestViewerViewModel()
        {
            RequestsListingViewModel = new RequestsListingViewModel();
            RequestsDetailsViewModel = new RequestsDetailsViewModel();
        }
    }
}
