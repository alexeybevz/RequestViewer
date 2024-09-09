using System;
using System.Collections.Generic;
using System.Linq;

namespace RequestViewer.Domain.Models
{
    public class Request
    {
        public Guid Id => Guid.NewGuid();
        public string UserName { get; set; }
        public string ActiveDirectoryCN { get; set; }
        public Period Period { get; set; }
        public string Name => IsApprovedStr + " на период " + Period.Range;
        public List<Day> Dates { get; set; }
        public string DatesStr => string.Join(" / ", Dates.Select(x => x.Date.ToString("dd.MM.yyyy")));
        public bool IsApproved { get; set; }
        public string IsApprovedStr => IsApproved ? "Согласованные заявки" : "Заявки на открытие доступа";
    }
}
