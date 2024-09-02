using System.Collections.ObjectModel;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestsDetailsViewModel : ViewModelBase
    {
        private ObservableCollection<DayViewModel> _days;
        public ObservableCollection<DayViewModel> DayVMs
        {
            get { return _days; }
            set { _days = value; OnPropertyChanged(nameof(DayVMs)); }
        }

        public RequestsDetailsViewModel()
        {
            DayVMs = new ObservableCollection<DayViewModel>();

            DayVMs.Add(new DayViewModel());
            DayVMs.Add(new DayViewModel());
            DayVMs.Add(new DayViewModel());
            DayVMs.Add(new DayViewModel());
            DayVMs.Add(new DayViewModel());
            DayVMs.Add(new DayViewModel());

            for (int i = 1; i < 31; i++)
            {
                DayVMs.Add(new DayViewModel() { Day = new System.DateTime(2024, 9, i).ToString("dd.MM.yyyy") });
            }
        }
    }
}
