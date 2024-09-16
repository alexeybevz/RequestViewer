using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using RequestViewer.Domain.Models;
using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class ChoiceUsersViewModel : ViewModelBase
    {
        private readonly UsersStore _usersStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private ICollectionView _users;
        private string _filterString = string.Empty;
        private IEnumerable<User> _selectedUsers;

        public ICollectionView Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        
        public string FilterString
        {
            get => _filterString;
            set
            {
                _filterString = value;
                OnPropertyChanged(nameof(FilterString));
                Users.Refresh();
            }
        }

        public IEnumerable<User> SelectedUsers
        {
            get => _selectedUsers;
            set
            {
                _selectedUsers = value;
                OnPropertyChanged(nameof(SelectedUsers));
                UsersSelected?.Invoke(value);
            }
        }

        public event Action<IEnumerable<User>> UsersSelected;

        public ICommand SubmitCommand { get; }

        public ChoiceUsersViewModel(UsersStore usersStore, ModalNavigationStore modalNavigationStore)
        {
            _usersStore = usersStore;
            _usersStore.UsersLoaded += UsersStore_UsersLoaded;
            _modalNavigationStore = modalNavigationStore;

            SubmitCommand = new SubmitSelectUserCommand(this);
        }

        private void UsersStore_UsersLoaded()
        {
            var users = _usersStore?.Users == null
                ? new List<User>()
                : _usersStore.Users.OrderBy(u => u.ActiveDirectoryCN).ToList();

            var items = users.Select(u => new UserViewModel(u)).ToList();

            Users = CollectionViewSource.GetDefaultView(items);
            Users.Filter += OnUsersFiltered;
        }

        private bool OnUsersFiltered(object obj)
        {
            if (string.IsNullOrWhiteSpace(_filterString)) return true;

            var user = ((UserViewModel)obj).User.ActiveDirectoryCN;
            return user.ToLower().Contains(_filterString.ToLower());
        }
    }
}