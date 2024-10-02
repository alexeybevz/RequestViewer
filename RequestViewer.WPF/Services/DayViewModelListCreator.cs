using System.Collections.Generic;
using RequestViewer.Domain.Models;
using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Services
{
    public static class DayViewModelListCreator
    {
        public static IEnumerable<DayViewModel> DayModelsToDayViewModels(IEnumerable<DayModel> dayModels)
        {
            var vms = new List<DayViewModel>();

            foreach (var dm in dayModels)
            {
                vms.Add(new DayViewModel(dm.IsCanEdit)
                {
                    Day = dm.Day,
                    IsHeader = dm.IsHeader,
                    IsApproved = dm.IsApproved,
                    IsOpen = dm.IsOpen
                });
            }

            return vms;
        }
    }
}