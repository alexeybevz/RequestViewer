using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Stores;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using RequestViewer.WPF.Services;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestsDetailsViewModel : ViewModelBase
    {
        private ObservableCollection<DayViewModel> _days;
        private readonly SelectedRequestStore _selectedRequestStore;

        public ObservableCollection<DayViewModel> DayVMs
        {
            get => _days;
            set { _days = value; OnPropertyChanged(nameof(DayVMs)); }
        }

        public bool HasCommands => !_selectedRequestStore.SelectedRequest?.IsApproved ?? false;
        public bool HasSelectedRequest => _selectedRequestStore.SelectedRequest != null;

        public ICommand ApproveRequestCommand { get; }
        public ICommand RejectRequestCommand { get; }

        public RequestsDetailsViewModel(SelectedRequestStore selectedRequestStore, RequestsStore requestsStore)
        {
            DayVMs = new ObservableCollection<DayViewModel>();

            ApproveRequestCommand = new ApproveRequestCommand(this, requestsStore, selectedRequestStore);
            RejectRequestCommand = new RejectRequestCommand(this, requestsStore, selectedRequestStore);

            _selectedRequestStore = selectedRequestStore;
            _selectedRequestStore.SelectedRequestChanged += SelectedRequestStore_SelectedRequestChanged;
        }

        private void SelectedRequestStore_SelectedRequestChanged()
        {
            RefreshDayVMs();

            OnPropertyChanged(nameof(HasCommands));
            OnPropertyChanged(nameof(HasSelectedRequest));
        }

        private void RefreshDayVMs()
        {
            DayVMs.Clear();

            var vms = DayViewModelListCreator.Create(_selectedRequestStore.SelectedRequest, false).ToList();
            vms.ForEach(vm => DayVMs.Add(vm));
        }
    }
}
