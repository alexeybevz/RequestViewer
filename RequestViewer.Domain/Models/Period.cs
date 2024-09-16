using System;

namespace RequestViewer.Domain.Models
{
    public class Period
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Range => $"{StartDate:dd.MM.yyyy} - {EndDate:dd.MM.yyyy}";
        public bool IsEnabled { get; set; }
        public string EnabledString => IsEnabled ? "Закрыт" : "Открыт";
    }
}
