namespace RequestViewer.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public RequestViewerViewModel RequestViewerViewModel { get; }

        public MainViewModel()
        {
            RequestViewerViewModel = new RequestViewerViewModel();
        }
    }
}
