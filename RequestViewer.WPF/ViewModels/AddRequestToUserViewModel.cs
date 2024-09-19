using System;
using System.Windows;
using System.Windows.Input;
using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class AddRequestToUserViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public event Action OnExecuted;
        public event Action OnErrorOccurs;

        public ICommand OpenAddRequestToUserCommand { get; }

        public AddRequestToUserViewModel(string login, RequestsStore requestsStore, ModalNavigationStore modalNavigationStore, PeriodsStore periodsStore, UsersStore usersStore)
        {
            _modalNavigationStore = modalNavigationStore;

            var cmd = new OpenAddRequestToUserCommand(login, requestsStore, _modalNavigationStore, periodsStore, usersStore);
            cmd.OnUserNotFound += () =>
            {
                MessageBox.Show($"Пользователь с логином '{login}' не найден. Создание заявки отменено.");
                OnErrorOccurs?.Invoke();
            };
            OpenAddRequestToUserCommand = cmd;

            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStore_CurrentViewModelChanged;
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
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStore_CurrentViewModelChanged;

            base.Dispose();
        }
    }
}