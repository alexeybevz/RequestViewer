using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public RequestViewerViewModel RequestViewerViewModel { get; }

        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public MainViewModel(SelectedRequestStore selectedRequestStore, RequestsStore requestsStore, ModalNavigationStore modalNavigationStore, UsersStore usersStore, PeriodsStore periodsStore)
        {
            _modalNavigationStore = modalNavigationStore;
            RequestViewerViewModel = RequestViewerViewModel.LoadViewModel(requestsStore, selectedRequestStore, modalNavigationStore, usersStore, periodsStore);

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
