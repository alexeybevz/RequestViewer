using System;
using System.Windows;
using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class AddRequestToUserViewModel : ViewModelBase
    {
        private readonly string _login;
        private readonly ModalNavigationStore _modalNavigationStore;

        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public event Action<bool> OnExecuted;
        public event Action OnErrorOccurs;

        public OpenAddRequestToUserCommand OpenAddRequestToUserCommand { get; }

        public AddRequestToUserViewModel(string login, RequestsStore requestsStore, ModalNavigationStore modalNavigationStore, PeriodsStore periodsStore, UsersStore usersStore)
        {
            _login = login;
            _modalNavigationStore = modalNavigationStore;

            OpenAddRequestToUserCommand = new OpenAddRequestToUserCommand(_login, requestsStore, _modalNavigationStore, periodsStore, usersStore);
            OpenAddRequestToUserCommand.OnUserNotFound += OnUserNotFound;

            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStore_CurrentViewModelChanged;
            _modalNavigationStore.IsSubmitClosed += ModalNavigationStore_OnIsSubmitClosed;
            _modalNavigationStore.IsCancelClosed += ModalNavigationStore_OnIsCancelClosed;
        }

        private void ModalNavigationStore_OnIsCancelClosed()
        {
            OnExecuted?.Invoke(false);
        }

        private void ModalNavigationStore_OnIsSubmitClosed()
        {
            OnExecuted?.Invoke(true);
        }

        private void OnUserNotFound()
        {
            MessageBox.Show($"Пользователь с логином '{_login}' не найден. Создание заявки отменено.");
            OnErrorOccurs?.Invoke();
        }

        private void ModalNavigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }

        protected override void Dispose()
        {
            OpenAddRequestToUserCommand.OnUserNotFound -= OnUserNotFound;
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStore_CurrentViewModelChanged;

            _modalNavigationStore.IsSubmitClosed -= ModalNavigationStore_OnIsSubmitClosed;
            _modalNavigationStore.IsCancelClosed -= ModalNavigationStore_OnIsCancelClosed;

            base.Dispose();
        }
    }
}