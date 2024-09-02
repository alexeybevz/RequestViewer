using RequestViewer.Domain.Models;
using System;

namespace RequestViewer.WPF.Stores
{
    public class SelectedRequestStore
    {
        private Request _selectedRequest;
        private readonly RequestsStore _requestsStore;

        public Request SelectedRequest
        {
            get
            {
                return _selectedRequest;
            }
            set
            {
                _selectedRequest = value;
                SelectedRequestChanged?.Invoke();
            }
        }

        public event Action SelectedRequestChanged;

        public SelectedRequestStore(RequestsStore requestsStore)
        {
            _requestsStore = requestsStore;

            _requestsStore.RequestAdded += RequestsStore_RequestAdded;
            _requestsStore.RequestUpdated += RequestsStore_RequestUpdated;
            _requestsStore.RequestDeleted += RequestsStore_RequestDeleted;
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

        private void RequestsStore_RequestDeleted(Guid id)
        {
            if (SelectedRequest?.Id == id)
            {
                SelectedRequest = null;
            }
        }
    }
}
