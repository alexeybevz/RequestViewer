using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Commands
{
    public class ChangeDayStateCommand : CommandBase
    {
        private readonly DayViewModel _dayViewModel;

        public ChangeDayStateCommand(DayViewModel dayViewModel)
        {
            _dayViewModel = dayViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (!_dayViewModel.IsHeader && !string.IsNullOrEmpty(_dayViewModel.Day))
                _dayViewModel.IsOpen = !_dayViewModel.IsOpen;
        }

        public override bool CanExecute(object? parameter)
        {
            return _dayViewModel.IsCanEdit;
        }
    }
}
