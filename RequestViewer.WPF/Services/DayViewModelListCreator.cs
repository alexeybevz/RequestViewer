using System.Collections.Generic;
using System.Linq;
using RequestViewer.Domain.Models;
using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Services
{
    public static class DayViewModelListCreator
    {
        private static readonly string[] DaysOfWeekHeaders = {"ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС"};

        public static IEnumerable<DayViewModel> Create(Request request, bool isCanEdit)
        {
            var vms = new List<DayViewModel>();

            if (request == null)
                return vms;

            foreach (var header in DaysOfWeekHeaders)
                vms.Add(new DayViewModel(false) { Day = header, IsHeader = true });

            var dayOfWeek = (int)(request.Period.StartDate.DayOfWeek + 6) % 7;

            for (int i = 0; i < dayOfWeek; i++)
            {
                vms.Add(new DayViewModel(false));
            }

            for (int i = 1; i <= request.Period.EndDate.Day; i++)
            {
                var dt = new System.DateTime(request.Period.EndDate.Year, request.Period.EndDate.Month, i);

                vms.Add(new DayViewModel(isCanEdit)
                {
                    Day = dt.ToString("dd.MM.yyyy"),
                    IsHeader = false,
                    IsOpen = request.Dates?.Select(d => d.Date).Contains(dt) ?? false,
                    IsApproved = request.IsApproved
                });
            }

            return vms;
        }
    }
}