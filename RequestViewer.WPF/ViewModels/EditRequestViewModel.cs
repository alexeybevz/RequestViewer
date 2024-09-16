using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using RequestViewer.Domain.Models;
using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Services;
using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class EditRequestViewModel : ViewModelBase
    {
        private ObservableCollection<DayViewModel> _days;

        public ObservableCollection<DayViewModel> DayVMs
        {
            get => _days;
            set { _days = value; OnPropertyChanged(nameof(DayVMs)); }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public EditRequestViewModel(Request request, RequestsStore requestsStore, ModalNavigationStore modalNavigationStore)
        {
            DayVMs = new ObservableCollection<DayViewModel>();

            SubmitCommand = new EditRequestCommand(this, modalNavigationStore, requestsStore, request);
            CancelCommand = new CloseModalCommand(modalNavigationStore);

            RefreshDayVMs(request);
        }

        private void RefreshDayVMs(Request request)
        {
            DayVMs.Clear();

            var vms = DayViewModelListCreator.Create(request.Period, request.Dates, request.IsApproved, true).ToList();
            vms.ForEach(vm => DayVMs.Add(vm));
        }
    }
}
