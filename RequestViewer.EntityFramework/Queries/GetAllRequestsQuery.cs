using Microsoft.EntityFrameworkCore;
using RequestViewer.Domain.Models;
using RequestViewer.Domain.Queries;
using System;
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
            IEnumerable<User> users = await _getAllUsersQuery.Execute();

            using (var context = _contextFactory.Create())
            {
                var requests = await context.Requests.Include(r => r.Period).ToListAsync();

                var grp = requests.GroupBy(r => new { r.UserName, r.PeriodId, r.Period, r.IsApproved }).ToList();
                return grp.Select(r => new Request()
                {
                    Id = Guid.NewGuid(),
                    UserName = r.Key.UserName,
                    ActiveDirectoryCN = users.FirstOrDefault(u => u.Login == r.Key.UserName)?.ActiveDirectoryCN ?? r.Key.UserName,
                    Period = new Period()
                    {
                        PeriodId = r.Key.PeriodId,
                        StartDate = r.Key.Period.StartDate,
                        EndDate = r.Key.Period.EndDate,
                        IsEnabled = r.Key.Period.IsEnabled,
                    },
                    IsApproved = r.Key.IsApproved,
                    Dates = r.Select(g => new Day() { RequestId = g.Id, Date = g.AllowedDate }).ToList()
                })
                .OrderBy(x => x.IsApproved)
                .ThenBy(x => x.Period.PeriodId)
                .ThenBy(x => x.ActiveDirectoryCN)
                .ToList();
            }
        }
    }
}
