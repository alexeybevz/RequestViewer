using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using RequestViewer.Domain.Models;
using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class EditRequestViewModel : ViewModelBase
    {
        private readonly Request _request;
        private ObservableCollection<DayViewModel> _days;
        private readonly List<string> _daysOfWeekHeaders;

        public ObservableCollection<DayViewModel> DayVMs
        {
            get { return _days; }
            set { _days = value; OnPropertyChanged(nameof(DayVMs)); }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public EditRequestViewModel(Request request, RequestsStore requestsStore, ModalNavigationStore modalNavigationStore)
        {
            _request = request;
            DayVMs = new ObservableCollection<DayViewModel>();

            SubmitCommand = null;
            CancelCommand = new CloseModalCommand(modalNavigationStore);

            _daysOfWeekHeaders = new List<string>() { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС" };

            RefreshDayVMs();
        }

        private void RefreshDayVMs()
        {
            DayVMs.Clear();

            if (_request == null)
                return;

            foreach (var header in _daysOfWeekHeaders)
                DayVMs.Add(new DayViewModel(true) { Day = header, IsHeader = true });

            var dayOfWeek = (int)(_request.Period.StartDate.DayOfWeek + 6) % 7;

            for (int i = 0; i < dayOfWeek; i++)
            {
                DayVMs.Add(new DayViewModel(true));
            }

            for (int i = 1; i <= _request.Period.EndDate.Day; i++)
            {
                var dt = new System.DateTime(2024, _request.Period.EndDate.Month, i);

                DayVMs.Add(new DayViewModel(true)
                {
                    Day = dt.ToString("dd.MM.yyyy"),
                    IsHeader = false,
                    IsOpen = _request.Dates?.Select(d => d.Date).Contains(dt) ?? false,
                    IsApproved = _request.IsApproved
                });
            }
        }
    }
}
