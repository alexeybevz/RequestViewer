using RequestViewer.WPF.ViewModels;
using System;

namespace RequestViewer.WPF.Stores
{
    public class ModalNavigationStore
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        internal void Close()
        {
            CurrentViewModel = null;
        }

        public bool IsOpen => CurrentViewModel != null;

        public event Action CurrentViewModelChanged;
    }
}
