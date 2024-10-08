﻿using System;
using System.Linq;
using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;
using System.Threading.Tasks;
using RequestViewer.Domain.Models;

namespace RequestViewer.WPF.Commands
{
    public class AddRequestCommand : AsyncCommandBase
    {
        private readonly AddRequestViewModel _addRequestViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly RequestsStore _requestsStore;
        private readonly bool _isApproved;

        public AddRequestCommand(AddRequestViewModel addRequestViewModel, ModalNavigationStore modalNavigationStore, RequestsStore requestsStore, bool isApproved)
        {
            _addRequestViewModel = addRequestViewModel;
            _modalNavigationStore = modalNavigationStore;
            _requestsStore = requestsStore;
            _isApproved = isApproved;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _addRequestViewModel.ErrorMessage = null;
            _addRequestViewModel.IsSubmitting = true;

            await Task.Delay(500);

            var openDays = _addRequestViewModel.DayVMs.Where(vm => vm.IsOpen).Select(d => new Day { Date = DateTime.Parse(d.Day) }).ToList();
            var period = _addRequestViewModel.Period;
            var users = _addRequestViewModel.Users;

            try
            {
                foreach (var user in users)
                {
                    var request = new Request()
                    {
                        ActiveDirectoryCN = user.ActiveDirectoryCN,
                        UserName = user.Login,
                        Period = period,
                        IsApproved = _isApproved,
                        Dates = openDays
                    };

                    await _requestsStore.Add(request);
                }

                _modalNavigationStore.Close();
            }
            catch (Exception ex)
            {
                _addRequestViewModel.ErrorMessage = "Произошла ошибка при создании заявки:\n" + ex.Message;

                if (ex.InnerException?.Message.Contains(
                    "UNIQUE constraint failed: Requests.UserName, Requests.PeriodId") ?? false)
                {
                    _addRequestViewModel.ErrorMessage = "Произошла ошибка при создании заявки:\n" + "У пользователя уже существует заявка в данном периоде";
                }
            }
            finally
            {
                _addRequestViewModel.IsSubmitting = false;
            }
        }
    }
}