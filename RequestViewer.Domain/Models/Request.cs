using System.Collections.Generic;

namespace RequestViewer.Domain.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ActiveDirectoryCN { get; set; }
        public Period Period { get; set; }
        public string Name => IsApprovedStr + " | Период " + Period.Range;
        public List<Day> Dates { get; set; }
        public bool IsApproved { get; set; }
        public string IsApprovedStr => IsApproved ? "Согласованные" : "Не согласованные";
    }
}
