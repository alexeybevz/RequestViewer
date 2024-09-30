using RequestViewer.WPF.ViewModels;
using System;

namespace RequestViewer.WPF.Stores
{
    public class ModalNavigationStore
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        internal void Close(bool isCancel = false)
        {
            CurrentViewModel = null;

            if (isCancel)
            {
                IsCancelClosed?.Invoke();
                return;
            }

            IsSubmitClosed?.Invoke();
        }

        public bool IsOpen => CurrentViewModel != null;

        public event Action CurrentViewModelChanged;
        public event Action IsSubmitClosed;
        public event Action IsCancelClosed;
    }
}
