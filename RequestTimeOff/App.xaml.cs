using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Neleus.DependencyInjection.Extensions;
using RequestTimeOff.Models;
using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using RequestTimeOff.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace RequestTimeOff
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureServices();
            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            var wpfNavService = _serviceProvider.GetRequiredService<INavigationService>();
            MainWindow.Show();
            ((WPFNavigationService)wpfNavService).Service = ((MainWindow)MainWindow).NavigationService;
        }
        private void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<Calendar>();
            services.AddSingleton<CalendarViewModel>();
            services.AddSingleton<Holidays>();
            services.AddSingleton<HolidaysViewModel>();
            services.AddSingleton<Home>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<HomeAdmin>();
            services.AddSingleton<HomeAdminViewModel>();
            services.AddSingleton<Login>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<PendingRequests>();
            services.AddSingleton<PendingRequestsViewModel>();
            services.AddSingleton<Settings>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<UserEdit>();
            services.AddSingleton<UserEditViewModel>();
            services.AddSingleton<Users>();
            services.AddSingleton<UsersViewModel>();
            
            services.AddByName<IPage>()
                .Add<Login>("Login")
                .Add<Home>("Home")
                .Add<HomeAdmin>("HomeAdmin")
                .Build();

            services.AddSingleton<IRequestTimeOffRepository, RequestTimeOffContext>();
            services.AddDbContext<RequestTimeOffContext>(options => options.UseSqlite("TimeOff.sqlite"));
            services.AddSingleton<INavigationService, WPFNavigationService>();

            _serviceProvider = services.BuildServiceProvider();

        }
    }
}
