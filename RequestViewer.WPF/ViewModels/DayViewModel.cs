using System.Windows.Input;
using RequestViewer.WPF.Commands;

namespace RequestViewer.WPF.ViewModels
{
    public class DayViewModel : ViewModelBase
    {
        private string _day;
        private bool _isHeader;
        private bool _isOpen;
        private bool _isApproved;

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

        public bool IsApproved
        {
            get { return _isApproved; }
            set { _isApproved = value; OnPropertyChanged(nameof(IsApproved)); }
        }

        public bool IsCanEdit { get; }

        public ICommand ChangeDayStateCommand { get; }

        public DayViewModel(bool isCanEdit)
        {
            IsCanEdit = isCanEdit;

            ChangeDayStateCommand = new ChangeDayStateCommand(this);
        }
    }
}
