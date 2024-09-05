namespace RequestViewer.WPF.ViewModels
{
    public class DayViewModel : ViewModelBase
    {
        private string _day;
        private bool _isHeader;
        private bool _isOpen;

        public string Day
        {
            get { return _day; }
            set { _day = value; OnPropertyChanged(nameof(Day)); }
        }
        
        public bool IsHeader
        {
            get { return _isHeader; }
            set { _isHeader = value; OnPropertyChanged(nameof(IsHeader)); }
        }

        public bool IsOpen
        {
            get { return _isOpen; }
            set { _isOpen = value; OnPropertyChanged(nameof(IsOpen)); }
        }
    }
}
