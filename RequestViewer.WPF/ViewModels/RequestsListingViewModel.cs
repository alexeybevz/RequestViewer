using RequestViewer.WPF.Domains;
using RequestViewer.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestsListingViewModel : ViewModelBase
    {
        private ObservableCollection<Request> _requests;
        public ObservableCollection<Request> Requests
        {
            get { return _requests; }
            set { _requests = value; OnPropertyChanged(nameof(Requests)); }
        }

        private Request _selectedRequest;
        private readonly SelectedRequestStore _selectedRequestStore;

        public Request SelectedRequest
        {
            get { return _selectedRequest; }
            set {
                _selectedRequest = value;
                _selectedRequestStore.SelectedRequest = value;

                OnPropertyChanged(nameof(SelectedRequest));
            }
        }

        public RequestsListingViewModel(SelectedRequestStore selectedRequestStore)
        {
            _selectedRequestStore = selectedRequestStore;

            Requests = new ObservableCollection<Request>()
            {
                new Request()
                {
                    UserName = "admin",
                    ActiveDirectoryCN = "Админ А. Админов",
                    Period = new Period() { StartDate = new DateTime(2024, 8, 1), EndDate = new DateTime(2024, 8, 31), IsEnabled = false, PeriodId = 1 },
                    Dates = new List<DateTime>() { new DateTime(2024, 8, 1) },
                    IsApproved = true
                },
                new Request()
                {
                    UserName = "admin2",
                    ActiveDirectoryCN = "Админ2 А. Админов2",
                    Period = new Period() { StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), IsEnabled = false, PeriodId = 2 },
                    Dates = new List<DateTime>() { new DateTime(2024, 9, 1) },
                    IsApproved = true
                }
            };
        }
    }
}
