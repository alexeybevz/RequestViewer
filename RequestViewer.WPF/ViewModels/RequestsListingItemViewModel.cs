using RequestViewer.Domain.Models;
using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestsListingItemViewModel : ViewModelBase
    {
        public Request Request { get; private set; }
        public string Name => Request.Name;
        public string ActiveDirectoryCN => Request.ActiveDirectoryCN;
        public bool HasCommands => Request.IsApproved;

        public RequestsListingItemViewModel(Request request, RequestsStore requestsStore)
        {
            Request = request;
        }

        public void Update(Request request)
        {
            Request = request;

            OnPropertyChanged(nameof(Request));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(ActiveDirectoryCN));
        }
    }
}
