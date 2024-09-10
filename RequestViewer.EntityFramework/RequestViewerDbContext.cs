using Microsoft.EntityFrameworkCore;
using RequestViewer.EntityFramework.DTOs;

namespace RequestViewer.EntityFramework
{
    public class RequestViewerDbContext : DbContext
    {
        public RequestViewerDbContext(DbContextOptions options) : base(options) { }

        public DbSet<RequestDto> Requests { get; set; }
        public DbSet<PeriodDto> Periods { get; set; }
        public DbSet<UserDto> Users { get; set; }
    }
}
