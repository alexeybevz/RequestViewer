using RequestViewer.Domain.Models;
using System;

namespace RequestViewer.WPF.Stores
{
    public class SelectedRequestStore
    {
        private Request _selectedRequest;
        private Request _prevSelectedRequest;
        private readonly RequestsStore _requestsStore;

        public Request SelectedRequest
        {
            get
            {
                return _selectedRequest;
            }
            set
            {
                if (!Equals(_selectedRequest, value))
                {
                    _prevSelectedRequest = _selectedRequest;
                    _selectedRequest = value;
                    SelectedRequestChanged?.Invoke();
                    if (value == null)
                    {
                        // Ignore null set by the live grouping/sorting/filtering in the CollectionViewSource
                        System.Windows.Application.Current.MainWindow.Dispatcher.BeginInvoke(
                            new Action(() =>
                            {
                                SelectedRequest = _prevSelectedRequest;
                            }));
                    }
                }
            }
        }

        public event Action SelectedRequestChanged;

        public SelectedRequestStore(RequestsStore requestsStore)
        {
            _requestsStore = requestsStore;

            _requestsStore.RequestsLoaded += RequestsStore_RequestLoaded;
            _requestsStore.RequestAdded += RequestsStore_RequestAdded;
            _requestsStore.RequestUpdated += RequestsStore_RequestUpdated;
            _requestsStore.RequestDeleted += RequestsStore_RequestDeleted;
            _requestsStore.RequestApproved += RequestsStore_RequestApproved;
            _requestsStore.RequestRejected += RequestsStore_RequestRejected;
        }

        private void RequestsStore_RequestLoaded()
        {
            ClearSelectedRequest();
        }

        private void RequestsStore_RequestAdded(Request request)
        {
            SelectedRequest = request;
        }

        private void RequestsStore_RequestUpdated(Request request)
        {
            if (request.Id == SelectedRequest?.Id)
            {
                SelectedRequest = request;
            }
        }

        private void RequestsStore_RequestDeleted(int id)
        {
            if (SelectedRequest?.Id == id)
            {
                ClearSelectedRequest();
            }
        }

        private void RequestsStore_RequestApproved(Request request)
        {
            if (request.Id == SelectedRequest?.Id)
            {
                SelectedRequest = request;
            }
        }

        private void RequestsStore_RequestRejected(Request request)
        {
            if (SelectedRequest?.Id == request.Id)
            {
                ClearSelectedRequest();
            }
        }

        private void ClearSelectedRequest()
        {
            _selectedRequest = null;
            SelectedRequestChanged?.Invoke();
        }
    }
}
