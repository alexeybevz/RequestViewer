﻿using RequestViewer.Domain.Models;

namespace RequestViewer.WPF.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private bool _isSelected;

        public UserViewModel(User value)
        {
            User = value;
        }

        public User User { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public override string ToString()
        {
            return User.ActiveDirectoryCN;
        }
    }
}