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
        private bool _isExecuting;

        public ObservableCollection<DayViewModel> DayVMs
        {
            get => _days;
            set { _days = value; OnPropertyChanged(nameof(DayVMs)); }
        }

        public bool HasCommands => !_selectedRequestStore.SelectedRequest?.IsApproved ?? false;
        public bool HasSelectedRequest => _selectedRequestStore.SelectedRequest != null;

        public bool IsExecuting
        {
            get => _isExecuting;
            set
            {
                _isExecuting = value;
                OnPropertyChanged(nameof(IsExecuting));
            }
        }

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

        protected override void Dispose()
        {
            _selectedRequestStore.SelectedRequestChanged -= SelectedRequestStore_SelectedRequestChanged;
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
            OnPropertyChanged(nameof(DayVMs));

            var request = _selectedRequestStore.SelectedRequest;

            if (request == null)
                return;

            var vms = DayViewModelListCreator.Create(request.Period, request.Dates, request.IsApproved, false).ToList();
            vms.ForEach(vm => DayVMs.Add(vm));
        }
    }
}
