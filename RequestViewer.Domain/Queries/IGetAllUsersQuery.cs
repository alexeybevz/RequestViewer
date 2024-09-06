using RequestViewer.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RequestViewer.Domain.Queries
{
    public interface IGetAllUsersQuery
    {
        Task<IEnumerable<User>> Execute();
    }
}
