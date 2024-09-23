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
        private readonly IGetAllUsersQuery _getAllUsersQuery;

        public GetAllRequestsQuery(RequestViewerDbContextFactory contextFactory, IGetAllUsersQuery getAllUsersQuery)
        {
            _contextFactory = contextFactory;
            _getAllUsersQuery = getAllUsersQuery;
        }

        public async Task<IEnumerable<Request>> Execute()
        {
            await Task.Delay(500);

            IEnumerable<User> users = await _getAllUsersQuery.Execute();

            using (var context = _contextFactory.Create())
            {
                var requests = await context.Requests
                    .Include(r => r.RequestsDays)
                    .Include(r => r.Period)
                    .ToListAsync();

                return requests.Select(r => new Request()
                {
                    Id = r.Id,
                    UserName = r.UserName,
                    ActiveDirectoryCN = users.FirstOrDefault(u => u.Login == r.UserName)?.ActiveDirectoryCN ?? r.UserName,
                    Period = new Period()
                    {
                        Id = r.PeriodId,
                        StartDate = r.Period.StartDate,
                        EndDate = r.Period.EndDate,
                        IsEnabled = r.Period.IsEnabled,
                    },
                    IsApproved = r.IsApproved,
                    Dates = r.RequestsDays.Select(d => new Day() { Id = d.Id, Date = d.AllowedDate, RequestId = r.Id}).ToList()
                })
                .OrderBy(x => x.IsApproved)
                .ThenBy(x => x.Period.Id)
                .ThenBy(x => x.ActiveDirectoryCN)
                .ToList();
            }
        }
    }
}
