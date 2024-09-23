using System;
using System.Linq;
using System.Threading.Tasks;
using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Commands
{
    public class RejectAllRequestCommand : AsyncCommandBase
    {
        private readonly RequestsListingGroupItemViewModel _requestsListingGroupItemViewModel;
        private readonly RequestsStore _requestsStore;

        public RejectAllRequestCommand(RequestsListingGroupItemViewModel requestsListingGroupItemViewModel, RequestsStore requestsStore)
        {
            _requestsListingGroupItemViewModel = requestsListingGroupItemViewModel;
            _requestsStore = requestsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _requestsListingGroupItemViewModel.ErrorMessage = null;
            _requestsListingGroupItemViewModel.IsExecuting = true;

            await Task.Delay(500);

            try
            {
                var requests = _requestsStore.Requests.Where(x => x.Name == _requestsListingGroupItemViewModel.GroupName).ToList();
                foreach (var request in requests)
                {
                    await _requestsStore.Reject(request);
                }
            }
            catch (Exception ex)
            {
                _requestsListingGroupItemViewModel.ErrorMessage = "Произошла ошибка при массовом отклонении заявок:\n" + ex.Message;
            }
            finally
            {
                _requestsListingGroupItemViewModel.IsExecuting = false;
            }
        }
    }
}