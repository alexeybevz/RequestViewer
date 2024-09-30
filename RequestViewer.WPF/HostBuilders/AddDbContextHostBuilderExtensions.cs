using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RequestViewer.Domain.Commands;
using RequestViewer.Domain.Queries;
using RequestViewer.EntityFramework;
using RequestViewer.EntityFramework.Commands;
using RequestViewer.EntityFramework.Queries;
using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                string connectionString = context.Configuration.GetConnectionString("sqlite");

                services.AddSingleton<DbContextOptions>(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);
                services.AddSingleton<RequestViewerDbContextFactory>();

                services.AddSingleton<IGetAllPeriodsQuery, GetAllPeriodsQuery>();
                services.AddSingleton<IGetAllUsersQuery, GetAllUsersQuery>();
                services.AddSingleton<IGetAllRequestsQuery, GetAllRequestsQuery>();
                services.AddSingleton<ICreateRequestCommand, CreateRequestCommand>();
                services.AddSingleton<IUpdateRequestCommand, UpdateRequestCommand>();
                services.AddSingleton<IDeleteRequestCommand, DeleteRequestCommand>();
                services.AddSingleton<IApproveRequestCommand, ApproveRequestCommand>();
                services.AddSingleton<IRejectRequestCommand, RejectRequestCommand>();

                services.AddSingleton<ModalNavigationStore>();
                services.AddSingleton<PeriodsStore>();
                services.AddSingleton<RequestsStore>();
                services.AddSingleton<SelectedRequestStore>();
                services.AddSingleton<UsersStore>();
            });

            return hostBuilder;
        }
    }
}