using System;
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

        public AddRequestCommand(AddRequestViewModel addRequestViewModel, ModalNavigationStore modalNavigationStore, RequestsStore requestsStore)
        {
            _addRequestViewModel = addRequestViewModel;
            _modalNavigationStore = modalNavigationStore;
            _requestsStore = requestsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
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
                        IsApproved = true,
                        Dates = openDays
                    };

                    await _requestsStore.Add(request);
                }

                _modalNavigationStore.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
    }
}