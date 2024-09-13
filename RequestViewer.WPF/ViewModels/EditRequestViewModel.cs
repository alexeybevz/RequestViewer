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
        private ObservableCollection<DayViewModel> _days;
        private readonly List<string> _daysOfWeekHeaders;

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

            _daysOfWeekHeaders = new List<string>() { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС" };

            RefreshDayVMs(request);
        }

        private void RefreshDayVMs(Request request)
        {
            DayVMs.Clear();

            if (request == null)
                return;

            foreach (var header in _daysOfWeekHeaders)
                DayVMs.Add(new DayViewModel(true) { Day = header, IsHeader = true });

            var dayOfWeek = (int)(request.Period.StartDate.DayOfWeek + 6) % 7;

            for (int i = 0; i < dayOfWeek; i++)
            {
                DayVMs.Add(new DayViewModel(true));
            }

            for (int i = 1; i <= request.Period.EndDate.Day; i++)
            {
                var dt = new System.DateTime(request.Period.EndDate.Year, request.Period.EndDate.Month, i);

                DayVMs.Add(new DayViewModel(true)
                {
                    Day = dt.ToString("dd.MM.yyyy"),
                    IsHeader = false,
                    IsOpen = request.Dates?.Select(d => d.Date).Contains(dt) ?? false,
                    IsApproved = request.IsApproved
                });
            }
        }
    }
}
