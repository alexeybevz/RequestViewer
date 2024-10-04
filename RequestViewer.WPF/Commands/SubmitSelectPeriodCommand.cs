using System.Threading.Tasks;
using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Commands
{
    public class SubmitSelectPeriodCommand : AsyncCommandBase
    {
        private readonly ChoicePeriodViewModel _choicePeriodViewModel;

        public SubmitSelectPeriodCommand(ChoicePeriodViewModel choicePeriodViewModel)
        {
            _choicePeriodViewModel = choicePeriodViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (_choicePeriodViewModel.SelectedPeriod == null)
                return;

            _choicePeriodViewModel.RaisePeriodSelectedEvent();
        }
    }
}