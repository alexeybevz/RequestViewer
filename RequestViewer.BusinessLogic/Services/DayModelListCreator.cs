﻿using System.Collections.Generic;
using System.Linq;
using RequestViewer.Domain.Models;

namespace RequestViewer.BusinessLogic.Services
{
    public static class DayModelListCreator
    {
        private static readonly string[] DaysOfWeekHeaders = {"ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС"};

        public static IEnumerable<DayModel> Create(Period period, IList<Day> openDays, bool isApproved, bool isCanEdit)
        {
            var vms = new List<DayModel>();

            if (period == null)
                return vms;

            foreach (var header in DaysOfWeekHeaders)
                vms.Add(new DayModel(false) { Day = header, IsHeader = true });

            var dayOfWeek = (int)(period.StartDate.DayOfWeek + 6) % 7;

            for (int i = 0; i < dayOfWeek; i++)
            {
                vms.Add(new DayModel(false));
            }

            for (int i = 1; i <= period.EndDate.Day; i++)
            {
                var dt = new System.DateTime(period.EndDate.Year, period.EndDate.Month, i);


                vms.Add(new DayModel(isCanEdit)
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