using Microsoft.EntityFrameworkCore;
using RequestViewer.Domain.Models;
using RequestViewer.Domain.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestViewer.EntityFramework.Queries
{
    public class GetAllUsersQuery : IGetAllUsersQuery
    {
        private readonly RequestViewerDbContextFactory _contextFactory;

        public GetAllUsersQuery(RequestViewerDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<User>> Execute()
        {
            using (var context = _contextFactory.Create())
            {
                var users = await context.Users.ToListAsync();

                return users.Select(u => new User()
                {
                    Id = u.Id,
                    Login = u.Login,
                    ActiveDirectoryCN = u.ActiveDirectoryCN,
                    Email = u.Email
                }).ToList();
            }
        }
    }
}
