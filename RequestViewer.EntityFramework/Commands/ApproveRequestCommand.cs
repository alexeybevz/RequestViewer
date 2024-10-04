using RequestViewer.Domain.Commands;
using RequestViewer.Domain.Models;
using RequestViewer.EntityFramework.DTOs;
using System.Threading.Tasks;

namespace RequestViewer.EntityFramework.Commands
{
    public class ApproveRequestCommand : IApproveRequestCommand
    {
        private readonly RequestViewerDbContextFactory _contextFactory;

        public ApproveRequestCommand(RequestViewerDbContextFactory requestViewerDbContextFactory)
        {
            _contextFactory = requestViewerDbContextFactory;
        }

        public async Task Execute(Request request)
        {
            using (var context = _contextFactory.Create())
            {
                var requestDto = new RequestDto() { Id = request.Id };

                context.Attach(requestDto);
                requestDto.IsApproved = request.IsApproved;

                await context.SaveChangesAsync();
            }
        }
    }
}
