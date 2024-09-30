using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;
using System;
using System.Threading.Tasks;

namespace RequestViewer.WPF.Commands
{
    public class RejectRequestCommand : AsyncCommandBase
    {
        private readonly RequestsDetailsViewModel _requestsDetailsViewModel;
        private readonly RequestsStore _requestsStore;
        private readonly SelectedRequestStore _selectedRequestStore;

        public RejectRequestCommand(RequestsDetailsViewModel requestsDetailsViewModel, RequestsStore requestsStore, SelectedRequestStore selectedRequestStore)
        {
            _requestsDetailsViewModel = requestsDetailsViewModel;
            _requestsStore = requestsStore;
            _selectedRequestStore = selectedRequestStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _requestsDetailsViewModel.ErrorMessage = null;
            _requestsDetailsViewModel.IsExecuting = true;

            await Task.Delay(500);

            try
            {
                await _requestsStore.Reject(_selectedRequestStore.SelectedRequest);
            }
            catch (Exception ex)
            {
                _requestsDetailsViewModel.ErrorMessage = "Произошла ошибка при отклонении заявки:\n" + ex.Message;
            }
            finally
            {
                _requestsDetailsViewModel.IsExecuting = false;
            }
        }
    }
}
