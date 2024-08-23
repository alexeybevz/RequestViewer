using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestsListingViewModel : ViewModelBase
    {
        private ObservableCollection<TableRow> _rows;
        public ObservableCollection<TableRow> Rows
        {
            get { return _rows; }
            set { _rows = value; OnPropertyChanged(nameof(Rows)); }
        }

        private TableRow _selectedRow;
        public TableRow SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; OnPropertyChanged(nameof(SelectedRow)); }
        }

        public RequestsListingViewModel()
        {
            Rows = new ObservableCollection<TableRow>()
            {
                new TableRow()
                {
                    UserName = "admin",
                    ActiveDirectoryCN = "Админ А. Админов",
                    Period = new Period() { StartDate = new DateTime(2024, 8, 1), EndDate = new DateTime(2024, 8, 31), IsEnabled = false, PeriodId = 1 },
                    Dates = new List<DateTime>() { new DateTime(2024, 8, 1) },
                    IsApproved = true
                }
            };
        }
    }

    public class TableRow
    {
        public string UserName { get; set; }
        public string ActiveDirectoryCN { get; set; }
        public Period Period { get; set; }
        public string Name => IsApprovedStr + " на период " + Period.Range;
        public List<DateTime> Dates { get; set; }
        public string DatesStr => string.Join(" / ", Dates.Select(x => x.ToString("dd.MM.yyyy")));
        public bool IsApproved { get; set; }
        public string IsApprovedStr => IsApproved ? "Согласованные заявки" : "Заявки на открытие доступа";
    }

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
