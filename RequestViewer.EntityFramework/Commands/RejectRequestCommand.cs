using RequestViewer.Domain.Commands;
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

        public async Task Execute(int id)
        {
            using (var context = _contextFactory.Create())
            {
                var requestDto = new RequestDto() { Id = id };

                context.Remove(requestDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
