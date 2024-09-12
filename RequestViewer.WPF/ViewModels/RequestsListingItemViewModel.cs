using RequestViewer.Domain.Models;
using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Stores;
using System.Windows.Input;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestsListingItemViewModel : ViewModelBase
    {
        public Request Request { get; private set; }
        public string Name => Request.Name;
        public string ActiveDirectoryCN => Request.ActiveDirectoryCN;
        public bool HasCommands => Request.IsApproved;

        public ICommand EditRequestCommand { get; }
        public ICommand DeleteRequestCommand { get; }

        public RequestsListingItemViewModel(Request request, RequestsStore requestsStore, ModalNavigationStore modalNavigationStore, SelectedRequestStore selectedRequestStore)
        {
            Request = request;

            EditRequestCommand = new OpenEditRequestCommand(this, requestsStore, modalNavigationStore, selectedRequestStore);
            //DeleteRequestCommand = new DeleteYouTubeViewerCommand(this, youTubeViewersStore);
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
