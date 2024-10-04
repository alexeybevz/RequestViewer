using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RequestViewer.EntityFramework
{
    public class RequestViewerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<RequestViewerDbContext>
    {
        public RequestViewerDbContext CreateDbContext(string[] args)
        {
            return new RequestViewerDbContext(new DbContextOptionsBuilder().UseSqlite("Data Source=RequestViewer.db").Options);
        }
    }
}
