using System.Threading.Tasks;
using RequestViewer.Domain.Commands;
using RequestViewer.Domain.Models;

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
            }
        }
    }
}