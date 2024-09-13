using System.Linq;
using System.Threading.Tasks;
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
                var requests = context.Requests
                    .Where(r => r.UserName == request.UserName && r.PeriodId == request.Period.PeriodId)
                    .ToList();

                // delete closed days
                foreach (var req in requests)
                {
                    if (days.Contains(req.AllowedDate))
                        continue;

                    context.Remove(req);
                }

                // add opened days
                foreach (var day in days)
                {
                    if (requests.Exists(r => r.AllowedDate == day))
                        continue;

                    var requestDto = new RequestDto()
                    {
                        UserName = request.UserName,
                        AllowedDate = day,
                        IsApproved = request.IsApproved,
                        PeriodId = request.Period.PeriodId,
                    };

                    context.Add(requestDto);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}