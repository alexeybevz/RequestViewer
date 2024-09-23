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

        public DeleteRequestCommand(RequestsListingItemViewModel requestsListingItemViewModel, RequestsStore requestsStore)
        {
            _requestsListingItemViewModel = requestsListingItemViewModel;
            _requestsStore = requestsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _requestsListingItemViewModel.IsDeleting = true;

            await Task.Delay(500);

            try
            {
                await _requestsStore.Delete(_requestsListingItemViewModel.Request.Id);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _requestsListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
