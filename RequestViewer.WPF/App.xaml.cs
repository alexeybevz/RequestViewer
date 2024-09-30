using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RequestViewer.EntityFramework;
using RequestViewer.WPF.Stores;
using RequestViewer.WPF.ViewModels;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using RequestViewer.WPF.HostBuilders;

namespace RequestViewer.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host
                .CreateDefaultBuilder()
                .AddDbContext()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<RequestViewerViewModel>(CreateRequestViewerViewModel);
                    services.AddSingleton<MainViewModel>();
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            var requestViewerDbContextFactory = _host.Services.GetRequiredService<RequestViewerDbContextFactory>();
            using (RequestViewerDbContext context = requestViewerDbContextFactory.Create())
            {
                context.Database.Migrate();
            }

            string key = "";
            string login = "";

            if (e.Args.Length == 2)
            {
                key = e.Args[0];
                login = e.Args[1];
            }

            if (key == "-createRequest")
            {
                var vm = new AddRequestToUserViewModel(
                    login,
                    _host.Services.GetRequiredService<RequestsStore>(),
                    _host.Services.GetRequiredService<ModalNavigationStore>(),
                    _host.Services.GetRequiredService<PeriodsStore>(),
                    _host.Services.GetRequiredService<UsersStore>());

                MainWindow = new AddRequestToUserWindow { DataContext = vm };

                vm.OnErrorOccurs += () =>
                {
                    MainWindow.Close();
                };

                vm.OnExecuted += (bool isExecuted) =>
                {
                    if (isExecuted)
                        MessageBox.Show("Заявка на открытие доступа отправлена и будет рассмотрена в течение дня.");

                    MainWindow.Close();
                };
                
                MainWindow.Loaded += (sender, args) =>
                {
                    vm.OpenAddRequestToUserCommand?.Execute(null);
                };
            }
            else
            {
                MainWindow = new MainWindow() { DataContext = _host.Services.GetRequiredService<MainViewModel>() };
            }

            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();
        }

        private RequestViewerViewModel CreateRequestViewerViewModel(IServiceProvider services)
        {
            return RequestViewerViewModel.LoadViewModel(
                services.GetRequiredService<RequestsStore>(),
                services.GetRequiredService<SelectedRequestStore>(),
                services.GetRequiredService<ModalNavigationStore>(),
                services.GetRequiredService<UsersStore>(),
                services.GetRequiredService<PeriodsStore>()
            );
        }
    }
}
