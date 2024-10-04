using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RequestViewer.Domain.Commands;
using RequestViewer.Domain.Models;
using RequestViewer.EntityFramework.DTOs;

namespace RequestViewer.EntityFramework.Commands
{
    public class UpdateRequestCommand : IUpdateRequestCommand
    {
        private readonly RequestViewerDbContextFactory _requestViewerDbContextFactory;

        public UpdateRequestCommand(RequestViewerDbContextFactory requestViewerDbContextFactory)
        {
            _requestViewerDbContextFactory = requestViewerDbContextFactory;
        }

        public async Task Execute(Request request)
        {
            var days = request.Dates.Select(x => x.Date).ToList();

            using (var context = _requestViewerDbContextFactory.Create())
            {
                var requestDays = context.Requests
                    .Include(r => r.RequestsDays)
                    .Where(r => r.Id == request.Id)
                    .SelectMany(r => r.RequestsDays).ToList()
                    .ToList();

                // delete closed days
                foreach (var reqDay in requestDays)
                {
                    if (days.Contains(reqDay.AllowedDate))
                        continue;

                    context.Remove(reqDay);
                }

                // add opened days
                foreach (var day in days)
                {
                    if (requestDays.Exists(r => r.AllowedDate == day))
                        continue;

                    var requestDayDto = new RequestDayDto()
                    {
                        RequestId = request.Id,
                        AllowedDate = day,
                    };

                    context.Add(requestDayDto);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}