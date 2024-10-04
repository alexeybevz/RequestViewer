using System.Threading.Tasks;
using RequestViewer.Domain.Commands;
using RequestViewer.EntityFramework.DTOs;

namespace RequestViewer.EntityFramework.Commands
{
    public class DeleteRequestCommand : IDeleteRequestCommand
    {
        private readonly RequestViewerDbContextFactory _contextFactory;

        public DeleteRequestCommand(RequestViewerDbContextFactory requestViewerDbContextFactory)
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