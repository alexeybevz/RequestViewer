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

        public event Action OnExecuted;
        public event Action OnErrorOccurs;

        public OpenAddRequestToUserCommand OpenAddRequestToUserCommand { get; }

        public AddRequestToUserViewModel(string login, RequestsStore requestsStore, ModalNavigationStore modalNavigationStore, PeriodsStore periodsStore, UsersStore usersStore)
        {
            _login = login;
            _modalNavigationStore = modalNavigationStore;

            OpenAddRequestToUserCommand = new OpenAddRequestToUserCommand(_login, requestsStore, _modalNavigationStore, periodsStore, usersStore);
            OpenAddRequestToUserCommand.OnUserNotFound += OnUserNotFound;

            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStore_CurrentViewModelChanged;
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

            if (CurrentModalViewModel == null)
                OnExecuted?.Invoke();
        }

        protected override void Dispose()
        {
            OpenAddRequestToUserCommand.OnUserNotFound -= OnUserNotFound;
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStore_CurrentViewModelChanged;

            base.Dispose();
        }
    }
}