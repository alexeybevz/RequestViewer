using System.Threading.Tasks;
using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Commands
{
    public class OpenAddRequestCommand : AsyncCommandBase
    {
        private readonly UsersStore _usersStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddRequestCommand(UsersStore usersStore, ModalNavigationStore modalNavigationStore)
        {
            _usersStore = usersStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            //var vm = new AddRequestViewModel(_requestsStore, _modalNavigationStore);
            var vm = new ChoiceUsersViewModel(_usersStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = vm;

            await _usersStore.Load();
        }
    }
}