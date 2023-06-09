﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Neleus.DependencyInjection.Extensions;
using NLog.Extensions.Logging;
using RequestTimeOff.Core.AutoMapper;
using RequestTimeOff.Core.Models.Requests;
using RequestTimeOff.Core.MVVM;
using RequestTimeOff.Models;
using RequestTimeOff.Models.Date;
using RequestTimeOff.Models.HomePages;
using RequestTimeOff.Models.MessageBoxes;
using RequestTimeOff.Models.Requests;
using RequestTimeOff.Models.Sessions;
using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using RequestTimeOff.Views;
using System;
using System.Runtime.CompilerServices;
using System.Windows;

[assembly: InternalsVisibleToAttribute("RequestTimeOff.Specflow")]
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
            services.AddSingleton<Departments>();
            services.AddSingleton<DepartmentsViewModel>();
            services.AddSingleton<Holidays>();
            services.AddSingleton<HolidaysViewModel>();
            services.AddSingleton<Home>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<HomePage>();
            services.AddSingleton<HomePageViewModel>();
            services.AddSingleton<HomePageAdmin>();
            services.AddSingleton<HomePageAdminViewModel>();
            services.AddSingleton<HomeAdmin>();
            services.AddSingleton<HomeAdminViewModel>();
            services.AddSingleton<Login>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<NewRequest>();
            services.AddSingleton<NewRequestViewModel>();
            services.AddSingleton<PendingRequests>();
            services.AddSingleton<PendingRequestsViewModel>();
            services.AddSingleton<Settings>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<UserCalendar>();
            services.AddSingleton<UserCalendarViewModel>();
            services.AddSingleton<UserEdit>();
            services.AddSingleton<UserEditViewModel>();
            services.AddSingleton<Users>();
            services.AddSingleton<UsersViewModel>();
            
            services.AddByName<IPage>()
                .Add<Calendar>("Calendar")
                .Add<Departments>("Departments")
                .Add<Holidays>("Holidays")
                .Add<Home>("Home")
                .Add<HomeAdmin>("HomeAdmin")
                .Add<HomePage>("HomePage")
                .Add<HomePageAdmin>("HomePageAdmin")
                .Add<Login>("Login")
                .Add<NewRequest>("NewRequest")
                .Add<PendingRequests>("PendingRequests")
                .Add<Settings>("Settings")
                .Add<UserCalendar>("UserCalendar")
                .Add<UserEdit>("UserEdit")
                .Add<Users>("Users")
                .Build();

            services.AddTransient<TimeOffValidator>();
            services.AddTransient<IValidator<TimeOff>, TimeOffValidator>();

            services.AddByName<IValidator>()
                .Add<TimeOffValidator>("TimeOff")
                .Build();

            services.AddSingleton<IRequestTimeOffRepository, RequestTimeOffContext>();
            services.AddDbContext<RequestTimeOffContext>(options => options.UseSqlite("TimeOff.sqlite"));
            services.AddSingleton<Session>();
            services.AddSingleton<INavigationService, WPFNavigationService>();
            services.AddTransient<IMessageBox, WPFMessageBox>();
            services.AddTransient<IDialogHost, WPFDialogHost>();
            services.AddTransient<IOpenDialog, OpenDialog>();

            services.AddTransient<ISessionLoad, SessionLoad>();
            services.AddSingleton<ISystemDateTime, SystemDateTime>();
            services.AddTransient<IUserYearInfo, UserYearInfo>();

            services.AddTransient<ITimeOffDateRange, TimeOffDateRange>();
            
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddLogging(builder => {
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddNLog();
                });
            _serviceProvider = services.BuildServiceProvider();

        }
    }
}
