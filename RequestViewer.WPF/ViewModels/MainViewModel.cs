using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly SelectedRequestStore _selectedRequestStore;
        private RequestsStore _requestsStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly UsersStore _usersStore;

        public RequestViewerViewModel RequestViewerViewModel { get; }

        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public MainViewModel(SelectedRequestStore selectedRequestStore, RequestsStore requestsStore, ModalNavigationStore modalNavigationStore, UsersStore usersStore)
        {
            _selectedRequestStore = selectedRequestStore;
            _requestsStore = requestsStore;
            _modalNavigationStore = modalNavigationStore;
            _usersStore = usersStore;
            RequestViewerViewModel = RequestViewerViewModel.LoadViewModel(requestsStore, selectedRequestStore, modalNavigationStore, usersStore);

            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStore_CurrentViewModelChanged;
        }

        private void ModalNavigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }

        protected override void Dispose()
        {
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStore_CurrentViewModelChanged;

            base.Dispose();
        }
    }
}
