using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RequestViewer.Domain.Models;
using RequestViewer.Domain.Queries;

namespace RequestViewer.EntityFramework.Queries
{
    public class GetAllPeriodsQuery : IGetAllPeriodsQuery
    {
        private readonly RequestViewerDbContextFactory _contextFactory;

        public GetAllPeriodsQuery(RequestViewerDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Period>> Execute()
        {
            using (var context = _contextFactory.Create())
            {
                var periods = await context.Periods.ToListAsync();

                return periods.Select(p => new Period()
                {
                    Id = p.Id,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    IsEnabled = p.IsEnabled,
                }).ToList();
            }
        }
    }
}