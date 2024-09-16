using System.Collections.Generic;
using System.Linq;
using RequestViewer.Domain.Models;
using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Services
{
    public static class DayViewModelListCreator
    {
        private static readonly string[] DaysOfWeekHeaders = {"ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС"};

        public static IEnumerable<DayViewModel> Create(Period period, IList<Day> openDays, bool isApproved, bool isCanEdit)
        {
            var vms = new List<DayViewModel>();

            if (period == null)
                return vms;

            foreach (var header in DaysOfWeekHeaders)
                vms.Add(new DayViewModel(false) { Day = header, IsHeader = true });

            var dayOfWeek = (int)(period.StartDate.DayOfWeek + 6) % 7;

            for (int i = 0; i < dayOfWeek; i++)
            {
                vms.Add(new DayViewModel(false));
            }

            for (int i = 1; i <= period.EndDate.Day; i++)
            {
                var dt = new System.DateTime(period.EndDate.Year, period.EndDate.Month, i);


                vms.Add(new DayViewModel(isCanEdit)
                {
                    Day = dt.ToString("dd.MM.yyyy"),
                    IsHeader = false,
                    IsOpen = openDays?.Select(d => d.Date).Contains(dt) ?? false,
                    IsApproved = isApproved
                });
            }

            return vms;
        }
    }
}