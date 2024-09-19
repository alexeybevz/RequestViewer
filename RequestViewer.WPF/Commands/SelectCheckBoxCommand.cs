using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Commands
{
    public class SelectCheckBoxCommand : CommandBase
    {
        private readonly CheckBoxViewModel _checkBoxViewModel;

        public SelectCheckBoxCommand(CheckBoxViewModel checkBoxViewModel)
        {
            _checkBoxViewModel = checkBoxViewModel;
        }

        public override void Execute(object? parameter)
        {
            _checkBoxViewModel.IsSelected = !_checkBoxViewModel.IsSelected;
        }
    }
}