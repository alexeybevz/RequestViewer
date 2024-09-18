using System;
using System.Linq;
using System.Threading.Tasks;
using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Commands
{
    public class DeleteAllRequestCommand : AsyncCommandBase
    {
        private readonly RequestsListingGroupItemViewModel _requestsListingGroupItemViewModel;
        private readonly RequestsStore _requestsStore;

        public DeleteAllRequestCommand(RequestsListingGroupItemViewModel requestsListingGroupItemViewModel, RequestsStore requestsStore)
        {
            _requestsListingGroupItemViewModel = requestsListingGroupItemViewModel;
            _requestsStore = requestsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                var requests = _requestsStore.Requests.Where(x => x.Name == _requestsListingGroupItemViewModel.GroupName).ToList();
                foreach (var request in requests)
                {
                    await _requestsStore.Delete(request.Id);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                
            }
        }
    }
}