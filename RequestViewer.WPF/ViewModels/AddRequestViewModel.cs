using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using RequestViewer.Domain.Models;
using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Services;
using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class AddRequestViewModel : ViewModelBase
    {
        private readonly RequestsStore _requestsStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly IEnumerable<User> _users;
        private readonly Period _period;

        public IEnumerable<User> Users => _users;
        public Period Period => _period;

        private ObservableCollection<DayViewModel> _days;

        public ObservableCollection<DayViewModel> DayVMs
        {
            get => _days;
            set { _days = value; OnPropertyChanged(nameof(DayVMs)); }
        }

        public bool CanSubmit => DayVMs.Count(x => x.IsOpen) > 0;

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public AddRequestViewModel(RequestsStore requestsStore, ModalNavigationStore modalNavigationStore, IEnumerable<User> users, Period period)
        {
            _requestsStore = requestsStore;
            _modalNavigationStore = modalNavigationStore;
            _users = users;
            _period = period;
            DayVMs = new ObservableCollection<DayViewModel>();

            SubmitCommand = new AddRequestCommand(this, modalNavigationStore, requestsStore);
            CancelCommand = new CloseModalCommand(modalNavigationStore);

            RefreshDayVMs(period);
        }

        private void RefreshDayVMs(Period period)
        {
            DayVMs.Clear();

            var vms = DayViewModelListCreator.Create(period, new List<Day>(), true, true).ToList();
            vms.ForEach(vm =>
            {
                vm.IsOpenChanged += () => OnPropertyChanged(nameof(CanSubmit));
                DayVMs.Add(vm);
            });
        }
    }
}