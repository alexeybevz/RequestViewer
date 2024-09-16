using System.Linq;
using System.Threading.Tasks;
using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Commands
{
    public class SubmitSelectUserCommand : AsyncCommandBase
    {
        private readonly ChoiceUsersViewModel _choiceUsersViewModel;

        public SubmitSelectUserCommand(ChoiceUsersViewModel choiceUsersViewModel)
        {
            _choiceUsersViewModel = choiceUsersViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _choiceUsersViewModel.FilterString = "";
            var users = _choiceUsersViewModel.Users.OfType<UserViewModel>().Select(u => u.User).ToList();

            _choiceUsersViewModel.SelectedUsers = users;
        }
    }
}