using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;
using System;
using System.Threading.Tasks;

namespace RequestViewer.WPF.Commands
{
    public class ApproveRequestCommand : AsyncCommandBase
    {
        private readonly RequestsDetailsViewModel _requestsDetailsViewModel;
        private readonly RequestsStore _requestsStore;
        private readonly SelectedRequestStore _selectedRequestStore;

        public ApproveRequestCommand(RequestsDetailsViewModel requestsDetailsViewModel, RequestsStore requestsStore, SelectedRequestStore selectedRequestStore)
        {
            _requestsDetailsViewModel = requestsDetailsViewModel;
            _requestsStore = requestsStore;
            _selectedRequestStore = selectedRequestStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _requestsDetailsViewModel.ErrorMessage = null;
            _requestsDetailsViewModel.IsExecuting = true;
            _selectedRequestStore.SelectedRequest.IsApproved = true;

            await Task.Delay(500);

            try
            {
                await _requestsStore.Approve(_selectedRequestStore.SelectedRequest);
            }
            catch (Exception ex)
            {
                _selectedRequestStore.SelectedRequest.IsApproved = false;
                _requestsDetailsViewModel.ErrorMessage = "Произошла ошибка при согласовании заявки:\n" + ex.Message;
            }
            finally
            {
                _requestsDetailsViewModel.IsExecuting = false;
            }
        }
    }
}
