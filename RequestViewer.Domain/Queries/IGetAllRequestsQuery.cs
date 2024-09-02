using RequestViewer.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RequestViewer.Domain.Queries
{
    public interface IGetAllRequestsQuery
    {
        Task<IEnumerable<Request>> Execute();
    }
}
