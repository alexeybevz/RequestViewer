using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using RequestViewer.Domain.Models;
using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Commands
{
    public class OpenAddRequestCommand : AsyncCommandBase
    {
        private readonly UsersStore _usersStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly PeriodsStore _periodsStore;

        public OpenAddRequestCommand(UsersStore usersStore, ModalNavigationStore modalNavigationStore, PeriodsStore periodsStore)
        {
            _usersStore = usersStore;
            _modalNavigationStore = modalNavigationStore;
            _periodsStore = periodsStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var vm = new ChoiceUsersViewModel(_usersStore);
            _modalNavigationStore.CurrentViewModel = vm;
            vm.UsersSelected += OnUsersSelected;

            await _usersStore.Load();
        }

        private async void OnUsersSelected(IEnumerable<User> selectedUsers)
        {
            var vm = new ChoicePeriodViewModel(_periodsStore);
            _modalNavigationStore.CurrentViewModel = vm;
            vm.PeriodSelected += OnPeriodSelected;

            await _periodsStore.Load();
        }

        private void OnPeriodSelected(Period period)
        {
            MessageBox.Show(period.StartDate.ToShortDateString());
        }
    }
}