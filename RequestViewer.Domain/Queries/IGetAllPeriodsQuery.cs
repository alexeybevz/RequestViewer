using System.Collections.Generic;
using System.Threading.Tasks;
using RequestViewer.Domain.Models;

namespace RequestViewer.Domain.Queries
{
    public interface IGetAllPeriodsQuery
    {
        Task<IEnumerable<Period>> Execute();
    }
}