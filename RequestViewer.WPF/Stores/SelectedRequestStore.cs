using RequestViewer.Domain.Models;
using System;

namespace RequestViewer.WPF.Stores
{
    public class SelectedRequestStore
    {
        private Request _selectedRequest;
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
    }
}
