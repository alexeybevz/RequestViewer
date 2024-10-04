using Microsoft.EntityFrameworkCore;

namespace RequestViewer.EntityFramework
{
    public class RequestViewerDbContextFactory
    {
        private readonly DbContextOptions _options;

        public RequestViewerDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public RequestViewerDbContext Create()
        {
            return new RequestViewerDbContext(_options);
        }
    }
}
