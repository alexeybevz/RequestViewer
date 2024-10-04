using System;
using RequestViewer.Domain.Models;

namespace RequestViewer.WPF.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
                IsSelectedChanged?.Invoke();
            }
        }

        public User User { get; }

        public event Action IsSelectedChanged;

        public UserViewModel(User value)
        {
            User = value;
        }

        public override string ToString()
        {
            return User.ActiveDirectoryCN;
        }
    }
}