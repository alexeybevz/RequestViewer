using System.Threading.Tasks;
using RequestViewer.Domain.Commands;
using RequestViewer.Domain.Models;
using RequestViewer.EntityFramework.DTOs;

namespace RequestViewer.EntityFramework.Commands
{
    public class CreateRequestCommand : ICreateRequestCommand
    {
        private readonly RequestViewerDbContextFactory _requestViewerDbContextFactory;

        public CreateRequestCommand(RequestViewerDbContextFactory requestViewerDbContextFactory)
        {
            _requestViewerDbContextFactory = requestViewerDbContextFactory;
        }

        public async Task Execute(Request request)
        {
            using (var context = _requestViewerDbContextFactory.Create())
            {
                var requestDto = new RequestDto()
                {
                    UserName = request.UserName,
                    IsApproved = request.IsApproved,
                    PeriodId = request.Period.Id,
                };

                context.Requests.Add(requestDto);
                await context.SaveChangesAsync();
                request.Id = requestDto.Id;

                foreach (var day in request.Dates)
                {
                    day.RequestId = request.Id;

                    var requestDayDto = new RequestDayDto()
                    {
                        RequestId = day.RequestId,
                        AllowedDate = day.Date,
                    };

                    context.RequestDays.Add(requestDayDto);
                    await context.SaveChangesAsync();
                    day.Id = requestDayDto.Id;
                }
            }
        }
    }
}