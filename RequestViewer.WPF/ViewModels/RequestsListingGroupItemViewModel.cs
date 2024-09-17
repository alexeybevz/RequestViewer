using System.Windows.Input;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestsListingGroupItemViewModel : ViewModelBase
    {
        public string GroupName { get; }
        public bool IsApproved { get; }

        public ICommand DeleteAllRequestCommand { get; }
        public ICommand ApproveAllRequestCommand { get; }
        public ICommand RejectAllRequestCommand { get; }

        public RequestsListingGroupItemViewModel(string groupName, bool isApproved)
        {
            GroupName = groupName;
            IsApproved = isApproved;
        }

        public override string ToString()
        {
            return GroupName;
        }
    }
}