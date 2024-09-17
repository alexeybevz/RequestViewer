using System.Collections.Generic;
using System.Threading.Tasks;
using RequestViewer.Domain.Models;
using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Commands
{
    public class OpenAddRequestCommand : AsyncCommandBase
    {
        private readonly RequestsStore _requestsStore;
        private readonly UsersStore _usersStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly PeriodsStore _periodsStore;

        private IEnumerable<User> _selectedUsers;
        private Period _period;

        public OpenAddRequestCommand(RequestsStore requestsStore, UsersStore usersStore, ModalNavigationStore modalNavigationStore, PeriodsStore periodsStore)
        {
            _requestsStore = requestsStore;
            _usersStore = usersStore;
            _modalNavigationStore = modalNavigationStore;
            _periodsStore = periodsStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var vm = new ChoiceUsersViewModel(_usersStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = vm;
            vm.UsersSelected += OnUsersSelected;

            await _usersStore.Load();
        }

        private async void OnUsersSelected(IEnumerable<User> selectedUsers)
        {
            _selectedUsers = selectedUsers;

            var vm = new ChoicePeriodViewModel(_periodsStore);
            _modalNavigationStore.CurrentViewModel = vm;
            vm.PeriodSelected += OnPeriodSelected;

            await _periodsStore.Load();
        }

        private void OnPeriodSelected(Period period)
        {
            _period = period;

            var vm = new AddRequestViewModel(_requestsStore, _modalNavigationStore, _selectedUsers, _period);
            _modalNavigationStore.CurrentViewModel = vm;
        }
    }
}