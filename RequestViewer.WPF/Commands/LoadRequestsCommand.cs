using System;
using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;
using System.Threading.Tasks;

namespace RequestViewer.WPF.Commands
{
    public class LoadRequestsCommand : AsyncCommandBase
    {
        private readonly RequestViewerViewModel _requestViewerViewModel;
        private readonly RequestsStore _requestsStore;

        public LoadRequestsCommand(RequestViewerViewModel requestViewerViewModel, RequestsStore requestsStore)
        {
            _requestViewerViewModel = requestViewerViewModel;
            _requestsStore = requestsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _requestViewerViewModel.ErrorMessage = null;
            _requestViewerViewModel.IsLoading = true;

            try
            {
                await _requestsStore.Load();
            }
            catch (Exception ex)
            {
                _requestViewerViewModel.ErrorMessage = "Произошла ошибка при загрузке заявок:\n\n" + ex.Message;
            }
            finally
            {
                _requestViewerViewModel.IsLoading = false;
            }
        }
    }
}
