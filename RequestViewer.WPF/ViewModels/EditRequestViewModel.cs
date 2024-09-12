using System.Windows.Input;
using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class EditRequestViewModel : ViewModelBase
    {
        public string Text => "123";

        public ICommand CloseModalCommand { get; }

        public EditRequestViewModel(ModalNavigationStore modalNavigationStore)
        {
            CloseModalCommand = new CloseModalCommand(modalNavigationStore);
        }
    }
}
