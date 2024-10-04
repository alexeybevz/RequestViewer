using RequestViewer.Domain.Models;
using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Stores;
using System.Windows.Input;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestsListingItemViewModel : ViewModelBase
    {
        private readonly RequestsStore _requestsStore;
        private bool _isDeleting;
        private string _errorMessage;

        public Request Request { get; private set; }
        public RequestsListingGroupItemViewModel Name { get; private set; }
        public string SortProperty { get; private set; }
        public string ActiveDirectoryCN => Request.ActiveDirectoryCN;
        public bool HasCommands => Request.IsApproved;

        public bool IsDeleting
        {
            get => _isDeleting;
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand EditRequestCommand { get; }
        public ICommand DeleteRequestCommand { get; }

        public RequestsListingItemViewModel(Request request, RequestsStore requestsStore, ModalNavigationStore modalNavigationStore, SelectedRequestStore selectedRequestStore, RequestsListingGroupItemViewModel requestsListingGroupItemViewModel)
        {
            _requestsStore = requestsStore;
            Request = request;

            Name = requestsListingGroupItemViewModel;
            SortProperty = request.IsApproved + request.Period.StartDate.ToShortDateString() + request.ActiveDirectoryCN;

            EditRequestCommand = new OpenEditRequestCommand(this, requestsStore, modalNavigationStore);
            DeleteRequestCommand = new DeleteRequestCommand(this, requestsStore);
        }

        public void Update(Request request)
        {
            Request = request;
            SortProperty = request.IsApproved + request.Period.StartDate.ToShortDateString() + request.ActiveDirectoryCN;

            OnPropertyChanged(nameof(Request));
            Name = new RequestsListingGroupItemViewModel(request, _requestsStore);
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(ActiveDirectoryCN));
            OnPropertyChanged(nameof(SortProperty));
        }
    }
}
