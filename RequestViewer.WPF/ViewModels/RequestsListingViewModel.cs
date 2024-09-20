using RequestViewer.Domain.Models;
using RequestViewer.WPF.Stores;
using System.Collections.ObjectModel;
using System.Linq;

namespace RequestViewer.WPF.ViewModels
{
    public class RequestsListingViewModel : ViewModelBase
    {
        private readonly SelectedRequestStore _selectedRequestStore;
        private readonly RequestsStore _requestsStore;
        private readonly ModalNavigationStore _modalNavigationStore;
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

        public RequestsListingViewModel(SelectedRequestStore selectedRequestStore, RequestsStore requestsStore, ModalNavigationStore modalNavigationStore)
        {
            _selectedRequestStore = selectedRequestStore;
            _requestsStore = requestsStore;
            _modalNavigationStore = modalNavigationStore;
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
            _selectedRequestStore.SelectedRequestChanged -= SelectedRequestStore_SelectedRequestChanged;

            _requestsStore.RequestsLoaded -= RequestsStore_RequestsLoaded;
            _requestsStore.RequestAdded -= RequestsStore_RequestAdded;
            _requestsStore.RequestUpdated -= RequestsStore_RequestUpdated;
            _requestsStore.RequestDeleted -= RequestsStore_RequestDeleted;
            _requestsStore.RequestApproved -= RequestsStore_RequestApproved;
            _requestsStore.RequestRejected -= RequestsStore_RequestRejected;

            _requestsListingItemViewModels.CollectionChanged -= RequestsListingItemViewModels_CollectionChanged;

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

        private void RequestsStore_RequestDeleted(int id)
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

            ReGroupingRequests(request.Name, request.ActiveDirectoryCN);
            OnPropertyChanged(nameof(SelectedRequestsListingItemViewModel));
        }

        private void RequestsStore_RequestRejected(Request request)
        {
            var vm = _requestsListingItemViewModels.FirstOrDefault(x => x.Request?.Id == request.Id);

            if (vm != null)
            {
                _requestsListingItemViewModels.Remove(vm);
            }
        }

        private void AddRequest(Request request)
        {
            var requestsListingGroupItemViewModel = new RequestsListingGroupItemViewModel(request, _requestsStore);
            var requestsListingItemViewModel = new RequestsListingItemViewModel(request, _requestsStore, _modalNavigationStore, _selectedRequestStore, requestsListingGroupItemViewModel);
            _requestsListingItemViewModels.Add(requestsListingItemViewModel);
        }

        private void RequestsListingItemViewModels_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedRequestsListingItemViewModel));
        }

        private void ReGroupingRequests(string groupName, string activeDirectoryCN)
        {
            var vms = _requestsListingItemViewModels.Where(x => x.Name.GroupName == groupName && x.ActiveDirectoryCN == activeDirectoryCN).ToList();
            if (vms == null || vms.Count <= 1)
                return;

            var dates = vms.SelectMany(x => x.Request.Dates).ToList();

            foreach (var vm in vms.SkipLast(1).ToList())
            {
                _requestsListingItemViewModels.Remove(vm);
            }

            var obj = vms.Last();
            obj.Request.Dates = dates;

            _selectedRequestStore.SelectedRequest = obj.Request;
        }
    }
}
