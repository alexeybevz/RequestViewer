using System.Windows.Input;
using RequestViewer.Domain.Models;
using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestsListingGroupItemViewModel : ViewModelBase
    {
        private string _groupName;
        private bool _isApproved;
        private bool _isExecuting;

        public string GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value;
                OnPropertyChanged(nameof(GroupName));
            }
        }

        public bool IsApproved
        {
            get => _isApproved;
            set
            {
                _isApproved = value;
                OnPropertyChanged(nameof(IsApproved));
            }
        }

        public bool IsExecuting
        {
            get => _isExecuting;
            set
            {
                _isExecuting = value;
                OnPropertyChanged(nameof(IsExecuting));
            }
        }
        
        public ICommand DeleteAllRequestCommand { get; }
        public ICommand ApproveAllRequestCommand { get; }
        public ICommand RejectAllRequestCommand { get; }

        public RequestsListingGroupItemViewModel(Request request, RequestsStore requestsStore)
        {
            UpdateProperties(request);

            DeleteAllRequestCommand = new DeleteAllRequestCommand(this, requestsStore);
            ApproveAllRequestCommand = new ApproveAllRequestCommand(this, requestsStore);
            RejectAllRequestCommand = new RejectAllRequestCommand(this, requestsStore);
        }

        public void UpdateProperties(Request request)
        {
            GroupName = request.Name;
            IsApproved = request.IsApproved;
        }

        public override string ToString()
        {
            return GroupName;
        }

        public override int GetHashCode()
        {
            return GroupName.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is RequestsListingGroupItemViewModel p) return GroupName == p.GroupName;
            return false;
        }
    }
}