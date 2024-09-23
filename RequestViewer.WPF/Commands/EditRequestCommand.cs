using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RequestViewer.Domain.Models;
using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;

namespace RequestViewer.WPF.Commands
{
    public class EditRequestCommand : AsyncCommandBase
    {
        private readonly EditRequestViewModel _editRequestViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly RequestsStore _requestsStore;
        private readonly Request _request;

        public EditRequestCommand(EditRequestViewModel editRequestViewModel, ModalNavigationStore modalNavigationStore, RequestsStore requestsStore, Request request)
        {
            _editRequestViewModel = editRequestViewModel;
            _modalNavigationStore = modalNavigationStore;
            _requestsStore = requestsStore;
            _request = request;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _editRequestViewModel.ErrorMessage = null;
            _editRequestViewModel.IsSubmitting = true;

            await Task.Delay(500);

            var dayVms = _editRequestViewModel.DayVMs.Where(x => x.IsOpen).ToList();

            var request = new Request()
            {
                Id = _request.Id,
                ActiveDirectoryCN = _request.ActiveDirectoryCN,
                UserName = _request.UserName,
                Period = _request.Period,
                IsApproved = _request.IsApproved,
                Dates = new List<Day>()
            };

            foreach (var dayVm in dayVms)
            {
                request.Dates.Add(new Day()
                {
                    Date = DateTime.Parse(dayVm.Day)
                });
            }

            try
            {
                await _requestsStore.Update(request);

                _modalNavigationStore.Close();
            }
            catch (Exception ex)
            {
                _editRequestViewModel.ErrorMessage = "Произошла ошибка при редактировании заявки:\n" + ex.Message;
            }
            finally
            {
                _editRequestViewModel.IsSubmitting = false;
            }
        }
    }
}