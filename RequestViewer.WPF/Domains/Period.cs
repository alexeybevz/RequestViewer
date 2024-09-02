using System;

namespace RequestViewer.WPF.Domains
{
    public class Period
    {
        public int PeriodId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Range => $"{StartDate:dd.MM.yyyy} - {EndDate:dd.MM.yyyy}";
        public bool IsEnabled { get; set; }
        public string EnabledSring => IsEnabled ? "Закрыт" : "Открыт";
    }
}
