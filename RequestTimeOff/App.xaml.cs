using Microsoft.Extensions.DependencyInjection;
using Neleus.DependencyInjection.Extensions;
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

            services.AddTransient<Home>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<Login>();
            services.AddTransient<LoginViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();
            
            services.AddByName<IPage>()
                .Add<Login>("Login")
                .Add<Home>("Home")
                .Build();
            services.AddSingleton<INavigationService, WPFNavigationService>();

            _serviceProvider = services.BuildServiceProvider();

        }
    }
}
