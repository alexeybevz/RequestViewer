using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using RequestViewer.Domain.Models;
using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Commands
{
    public class OpenAddRequestToUserCommand : AsyncCommandBase
    {
        private readonly string _login;
        private readonly RequestsStore _requestsStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly PeriodsStore _periodsStore;
        private readonly UsersStore _usersStore;

        private IEnumerable<User> _selectedUsers;
        private Period _period;

        public event Action OnUserNotFound;

        public OpenAddRequestToUserCommand(string login, RequestsStore requestsStore, ModalNavigationStore modalNavigationStore, PeriodsStore periodsStore, UsersStore usersStore)
        {
            _login = login;
            _requestsStore = requestsStore;
            _modalNavigationStore = modalNavigationStore;
            _periodsStore = periodsStore;
            _usersStore = usersStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            await _usersStore.Load();
            var user = _usersStore.Users.FirstOrDefault(u => u.Login == _login);

            if (user == null)
            {
                OnUserNotFound?.Invoke();
                return;
            }

            _selectedUsers = new List<User> { user };

            var vm = new ChoicePeriodViewModel(_periodsStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = vm;
            vm.PeriodSelected += OnPeriodSelected;

            await _periodsStore.Load();
        }

        private void OnPeriodSelected(Period period)
        {
            _period = period;

            var vm = new AddRequestViewModel(_requestsStore, _modalNavigationStore, _selectedUsers, _period, false);
            _modalNavigationStore.CurrentViewModel = vm;
        }
    }
}