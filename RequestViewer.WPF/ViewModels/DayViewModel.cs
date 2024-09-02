namespace RequestViewer.WPF.ViewModels
{
    public class DayViewModel : ViewModelBase
    {
        private string _day;
        public string Day
        {
            get { return _day; }
            set { _day = value; OnPropertyChanged(nameof(Day)); }
        }
    }
}
