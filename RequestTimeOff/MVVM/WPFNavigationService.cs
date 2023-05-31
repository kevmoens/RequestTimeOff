using Microsoft.VisualBasic;
using Neleus.DependencyInjection.Extensions;
using RequestTimeOff.MVVM.Events;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace RequestTimeOff.MVVM
{
    public class WPFNavigationService : INavigationService
    {
        private readonly TaskCompletionSource<bool> _taskCompletionSource = new();
        private NavigationService _service;
        public NavigationService Service 
        { 
            get 
            { 
                return _service; 
            } 
            set 
            { 
                _service = value;
                if (Service != null)
                {
                    Service.Navigated += Service_Navigated;
                }
                _taskCompletionSource.SetResult(true); 
            } 
        }
        private readonly IServiceByNameFactory<IPage> _pageFactory;
        public WPFNavigationService(IServiceByNameFactory<IPage> pageFactory)
        {
            _pageFactory = pageFactory;
        }

        private void Service_Navigated(object sender, NavigationEventArgs e)
        {
            if (e?.Content != null && e.Content is FrameworkElement element)
            {
                if (element.DataContext is INavigationAware aware)
                {
                    aware.OnNavigated();
                }
            }
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
            NavigateTo(pageKey, new Dictionary<string, object>());
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
            ViewNavigateTo(pageKey, new Dictionary<string, object>());
        }
        public void ViewNavigateTo(string pageKey, Dictionary<string, object> parameters)
        {
            ViewNavigation viewNav = new();
            var view = _pageFactory.GetRequiredByName(pageKey);
            viewNav.Content = view;
            ViewNavigationPubSub.Instance.Publish(viewNav);
            if (((FrameworkElement)view).DataContext is INavigationAware viewModel)
            {
                viewModel.OnNavigatedTo(parameters);
            }
        }
    }
}
