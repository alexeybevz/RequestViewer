using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;
using System;
using System.Threading.Tasks;

namespace RequestViewer.WPF.Commands
{
    public class DeleteRequestCommand : AsyncCommandBase
    {
        private RequestsListingItemViewModel _requestsListingItemViewModel;
        private RequestsStore _requestsStore;
        private SelectedRequestStore _selectedRequestStore;

        public DeleteRequestCommand(RequestsListingItemViewModel requestsListingItemViewModel, RequestsStore requestsStore, SelectedRequestStore selectedRequestStore)
        {
            _requestsListingItemViewModel = requestsListingItemViewModel;
            _requestsStore = requestsStore;
            _selectedRequestStore = selectedRequestStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _requestsStore.Delete(_selectedRequestStore.SelectedRequest.Id);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
    }
}
