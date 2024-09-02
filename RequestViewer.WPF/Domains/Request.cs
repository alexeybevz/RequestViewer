using System;
using System.Collections.Generic;
using System.Linq;

namespace RequestViewer.WPF.Domains
{
    public class Request
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string ActiveDirectoryCN { get; set; }
        public Period Period { get; set; }
        public string Name => IsApprovedStr + " на период " + Period.Range;
        public List<DateTime> Dates { get; set; }
        public string DatesStr => string.Join(" / ", Dates.Select(x => x.ToString("dd.MM.yyyy")));
        public bool IsApproved { get; set; }
        public string IsApprovedStr => IsApproved ? "Согласованные заявки" : "Заявки на открытие доступа";
    }
}
