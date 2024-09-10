using RequestViewer.Domain.Models;
using RequestViewer.WPF.Stores;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestsListingViewModel : ViewModelBase
    {
        private readonly SelectedRequestStore _selectedRequestStore;
        private readonly RequestsStore _requestsStore;
        private ObservableCollection<RequestsListingItemViewModel> _requestsListingItemViewModels;

        public ObservableCollection<RequestsListingItemViewModel> RequestsListingItemViewModels => _requestsListingItemViewModels;

        public RequestsListingItemViewModel SelectedRequestsListingItemViewModel
        {
            get
            {
                return _requestsListingItemViewModels.FirstOrDefault(y => y.Request.Id == _selectedRequestStore.SelectedRequest?.Id);
            }
            set
            {
                _selectedRequestStore.SelectedRequest = value?.Request;
                OnPropertyChanged(nameof(SelectedRequestsListingItemViewModel));
            }
        }

        public RequestsListingViewModel(SelectedRequestStore selectedRequestStore, RequestsStore requestsStore)
        {
            _selectedRequestStore = selectedRequestStore;
            _requestsStore = requestsStore;

            _requestsListingItemViewModels = new ObservableCollection<RequestsListingItemViewModel>();

            _selectedRequestStore.SelectedRequestChanged += SelectedRequestStore_SelectedRequestChanged;

            _requestsStore.RequestsLoaded += RequestsStore_RequestsLoaded;
            _requestsStore.RequestAdded += RequestsStore_RequestAdded;
            _requestsStore.RequestUpdated += RequestsStore_RequestUpdated;
            _requestsStore.RequestDeleted += RequestsStore_RequestDeleted;
            _requestsStore.RequestApproved += RequestsStore_RequestApproved;
            _requestsStore.RequestRejected += RequestsStore_RequestRejected;

            _requestsListingItemViewModels.CollectionChanged += RequestsListingItemViewModels_CollectionChanged;
        }

        protected override void Dispose()
        {
            _requestsStore.RequestsLoaded -= RequestsStore_RequestsLoaded;
            _requestsStore.RequestAdded -= RequestsStore_RequestAdded;
            _requestsStore.RequestUpdated -= RequestsStore_RequestUpdated;
            _requestsStore.RequestDeleted -= RequestsStore_RequestDeleted;
            _requestsStore.RequestApproved -= RequestsStore_RequestApproved;
            _requestsStore.RequestRejected -= RequestsStore_RequestRejected;

            base.Dispose();
        }

        private void SelectedRequestStore_SelectedRequestChanged()
        {
            OnPropertyChanged(nameof(SelectedRequestsListingItemViewModel));
        }

        private void RequestsStore_RequestsLoaded()
        {
            _requestsListingItemViewModels.Clear();

            foreach (Request request in _requestsStore.Requests)
            {
                AddRequest(request);
            }
        }

        private void RequestsStore_RequestAdded(Request request)
        {
            AddRequest(request);
        }

        private void RequestsStore_RequestUpdated(Request request)
        {
            var vm = _requestsListingItemViewModels.FirstOrDefault(y => y.Request.Id == request.Id);

            if (vm != null)
            {
                vm.Update(request);
            }
        }

        private void RequestsStore_RequestDeleted(Guid id)
        {
            var vm = _requestsListingItemViewModels.FirstOrDefault(x => x.Request?.Id == id);

            if (vm != null)
            {
                _requestsListingItemViewModels.Remove(vm);
            }
        }

        private void RequestsStore_RequestApproved(Request request)
        {
            var vm = _requestsListingItemViewModels.FirstOrDefault(y => y.Request.Id == request.Id);

            if (vm != null)
            {
                vm.Update(request);
            }
        }

        private void RequestsStore_RequestRejected(Request request)
        {
            var vm = _requestsListingItemViewModels.FirstOrDefault(y => y.Request.Id == request.Id);

            if (vm != null)
            {
                vm.Update(request);
            }
        }

        private void AddRequest(Request request)
        {
            var vm = new RequestsListingItemViewModel(request, _requestsStore);
            _requestsListingItemViewModels.Add(vm);
        }

        private void RequestsListingItemViewModels_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedRequestsListingItemViewModel));
        }
    }
}
