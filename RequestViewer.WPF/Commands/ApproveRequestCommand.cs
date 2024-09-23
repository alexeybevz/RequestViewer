using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;
using System;
using System.Threading.Tasks;

namespace RequestViewer.WPF.Commands
{
    public class ApproveRequestCommand : AsyncCommandBase
    {
        private RequestsDetailsViewModel _requestsDetailsViewModel;
        private RequestsStore _requestsStore;
        private SelectedRequestStore _selectedRequestStore;

        public ApproveRequestCommand(RequestsDetailsViewModel requestsDetailsViewModel, RequestsStore requestsStore, SelectedRequestStore selectedRequestStore)
        {
            _requestsDetailsViewModel = requestsDetailsViewModel;
            _requestsStore = requestsStore;
            _selectedRequestStore = selectedRequestStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _requestsDetailsViewModel.IsExecuting = true;
            _selectedRequestStore.SelectedRequest.IsApproved = true;

            await Task.Delay(500);

            try
            {
                await _requestsStore.Approve(_selectedRequestStore.SelectedRequest);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _requestsDetailsViewModel.IsExecuting = false;
            }
        }
    }
}
