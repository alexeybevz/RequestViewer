using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class AddRequestViewModel : ViewModelBase
    {
        private readonly RequestsStore _requestsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public AddRequestViewModel(RequestsStore requestsStore, ModalNavigationStore modalNavigationStore)
        {
            _requestsStore = requestsStore;
            _modalNavigationStore = modalNavigationStore;
        }
    }
}