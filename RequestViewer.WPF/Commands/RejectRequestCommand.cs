using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;
using System.Threading.Tasks;

namespace RequestViewer.WPF.Commands
{
    public class RejectRequestCommand : AsyncCommandBase
    {
        private RequestsDetailsViewModel _requestsDetailsViewModel;
        private RequestsStore _requestsStore;
        private SelectedRequestStore _selectedRequestStore;

        public RejectRequestCommand(RequestsDetailsViewModel requestsDetailsViewModel, RequestsStore requestsStore, SelectedRequestStore selectedRequestStore)
        {
            _requestsDetailsViewModel = requestsDetailsViewModel;
            _requestsStore = requestsStore;
            _selectedRequestStore = selectedRequestStore;
        }

        public override Task ExecuteAsync(object parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}
