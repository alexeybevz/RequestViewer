using System;
using System.Linq;
using System.Threading.Tasks;
using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Commands
{
    public class ApproveAllRequestCommand : AsyncCommandBase
    {
        private readonly RequestsListingGroupItemViewModel _requestsListingGroupItemViewModel;
        private readonly RequestsStore _requestsStore;

        public ApproveAllRequestCommand(RequestsListingGroupItemViewModel requestsListingGroupItemViewModel, RequestsStore requestsStore)
        {
            _requestsListingGroupItemViewModel = requestsListingGroupItemViewModel;
            _requestsStore = requestsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _requestsListingGroupItemViewModel.IsExecuting = true;

            await Task.Delay(500);

            try
            {
                var requests = _requestsStore.Requests.Where(x => x.Name == _requestsListingGroupItemViewModel.GroupName).ToList();
                foreach (var request in requests)
                {
                    request.IsApproved = true;
                    await _requestsStore.Approve(request);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _requestsListingGroupItemViewModel.IsExecuting = false;
            }
        }
    }
}