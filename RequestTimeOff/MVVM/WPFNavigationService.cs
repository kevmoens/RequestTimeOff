using Microsoft.Extensions.DependencyInjection;
using Neleus.DependencyInjection.Extensions;
using RequestTimeOff.MVVM;
using RequestTimeOff.MVVM.Events;
using RequestTimeOff.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Navigation;

namespace RequestTimeOff.MVVM
{
    public class WPFNavigationService : INavigationService
    {
        private readonly TaskCompletionSource<bool> _taskCompletionSource = new TaskCompletionSource<bool>();
        private NavigationService _service;
        public NavigationService Service { get { return _service; } set { _service = value; _taskCompletionSource.SetResult(true); } }
        private readonly IServiceByNameFactory<IPage> _pageFactory;
        public WPFNavigationService(IServiceByNameFactory<IPage> pageFactory)
        {
            _pageFactory = pageFactory;
        }
        public async void GoBack()
        {
            await _taskCompletionSource.Task;
            if (Service.CanGoBack)
            {
                Service.GoBack();
            }
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, default);
        }

        public async void NavigateTo(string pageKey, Dictionary<string, object> parameters)
        {
            await _taskCompletionSource.Task;
            var page = _pageFactory.GetRequiredByName(pageKey);
            Service.Navigate(page);
            if (((FrameworkElement)page).DataContext is INavigationAware viewModel)
            {
                viewModel.OnNavigatedTo(parameters);
            }
        }
        public void ViewNavigateTo(string pageKey)
        {
            ViewNavigateTo(pageKey, default);
        }
        public void ViewNavigateTo(string pageKey, Dictionary<string, object> parameters)
        {
            ViewNavigation viewNav = new ViewNavigation();
            var view = _pageFactory.GetRequiredByName(pageKey);
            viewNav.Content = (FrameworkElement)view;
            ViewNavigationPubSub.Instance.Publish(viewNav);
            if (((FrameworkElement)view).DataContext is INavigationAware viewModel)
            {
                viewModel.OnNavigatedTo(parameters);
            }
        }
    }
}
