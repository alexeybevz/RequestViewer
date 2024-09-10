using RequestViewer.Domain.Commands;
using RequestViewer.Domain.Models;
using RequestViewer.EntityFramework.DTOs;
using System.Threading.Tasks;

namespace RequestViewer.EntityFramework.Commands
{
    public class RejectRequestCommand : IRejectRequestCommand
    {
        private readonly RequestViewerDbContextFactory _contextFactory;

        public RejectRequestCommand(RequestViewerDbContextFactory requestViewerDbContextFactory)
        {
            _contextFactory = requestViewerDbContextFactory;
        }

        public async Task Execute(Request request)
        {
            using (var context = _contextFactory.Create())
            {
                foreach (var day in request.Dates)
                {
                    var requestDto = new RequestDto() { Id = day.RequestId };

                    context.Remove(requestDto);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
