using Microsoft.EntityFrameworkCore;
using RequestViewer.Domain.Models;
using RequestViewer.Domain.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestViewer.EntityFramework.Queries
{
    public class GetAllRequestsQuery : IGetAllRequestsQuery
    {
        private readonly RequestViewerDbContextFactory _contextFactory;

        public GetAllRequestsQuery(RequestViewerDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Request>> Execute()
        {
            using (var context = _contextFactory.Create())
            {
                var requests = await context.Requests.Include(r => r.Period).ToListAsync();

                var grp = requests.GroupBy(r => new { r.Id, r.UserName, r.PeriodId, r.Period, r.IsApproved }).ToList();
                return grp.Select(r => new Request()
                {
                    Id = r.Key.Id,
                    UserName = r.Key.UserName,
                    Period = new Period()
                    {
                        PeriodId = r.Key.PeriodId,
                        StartDate = r.Key.Period.StartDate,
                        EndDate = r.Key.Period.EndDate,
                        IsEnabled = r.Key.Period.IsEnabled,
                    },
                    IsApproved = r.Key.IsApproved,
                    Dates = r.Select(g => g.AllowedDate).ToList()
                }).ToList();
            }
        }
    }
}
