using RequestViewer.WPF.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestsDetailsViewModel : ViewModelBase
    {
        private ObservableCollection<DayViewModel> _days;
        private readonly SelectedRequestStore _selectedRequestStore;
        private readonly List<string> _daysOfWeekHeaders;

        public ObservableCollection<DayViewModel> DayVMs
        {
            get { return _days; }
            set { _days = value; OnPropertyChanged(nameof(DayVMs)); }
        }

        public RequestsDetailsViewModel(SelectedRequestStore selectedRequestStore)
        {
            DayVMs = new ObservableCollection<DayViewModel>();

            _daysOfWeekHeaders = new List<string>() { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС" };

            _selectedRequestStore = selectedRequestStore;
            _selectedRequestStore.SelectedRequestChanged += SelectedRequestStore_SelectedRequestChanged;
        }

        private void SelectedRequestStore_SelectedRequestChanged()
        {
            DayVMs.Clear();

            foreach (var header in _daysOfWeekHeaders)
                DayVMs.Add(new DayViewModel() { Day = header, IsHeader = true });

            if (_selectedRequestStore.SelectedRequest == null)
                return;

            var dayOfWeek = (int)(_selectedRequestStore.SelectedRequest.Period.StartDate.DayOfWeek + 6) % 7;

            for (int i = 0; i < dayOfWeek; i++)
            {
                DayVMs.Add(new DayViewModel());
            }

            for (int i = 1; i <= _selectedRequestStore.SelectedRequest.Period.EndDate.Day; i++)
            {
                DayVMs.Add(new DayViewModel() { Day = new System.DateTime(2024, _selectedRequestStore.SelectedRequest.Period.EndDate.Month, i).ToString("dd.MM.yyyy"), IsHeader = false });
            }
        }
    }
}
