using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RequestViewer.Domain.Models;
using RequestViewer.Domain.Queries;

namespace RequestViewer.WPF.Stores
{
    public class UsersStore
    {
        private readonly IGetAllUsersQuery _getAllUsersQuery;
        private readonly List<User> _users;

        public IEnumerable<User> Users => _users;

        public event Action UsersLoaded;

        public UsersStore(IGetAllUsersQuery getAllUsersQuery)
        {
            _getAllUsersQuery = getAllUsersQuery;

            _users = new List<User>();
        }

        public async Task Load()
        {
            IEnumerable<User> users = await _getAllUsersQuery.Execute();

            _users.Clear();
            _users.AddRange(users);

            UsersLoaded?.Invoke();
        }
    }
}