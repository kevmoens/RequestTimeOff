using Microsoft.Extensions.DependencyInjection;
using Neleus.DependencyInjection.Extensions;
using RequestTimeOff.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Navigation;

namespace RequestTimeOff.MVVM
{
    public class WPFNavigationService : INavigationService
    {
        private TaskCompletionSource<bool> _taskCompletionSource = new TaskCompletionSource<bool>();
        private NavigationService _service;
        public NavigationService Service { get { return _service; } set { _service = value; _taskCompletionSource.SetResult(true); } }
        private IServiceByNameFactory<IPage> _pageFactory;
        public WPFNavigationService(IServiceByNameFactory<IPage> pageFactory)
        {
            _pageFactory = pageFactory;
        }
        public async void GoBack()
        {
            await _taskCompletionSource.Task;
            if (!Service.CanGoBack)
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
            page.ViewModel = ((FrameworkElement)page).DataContext as INavigationAware;
            Service.Navigate(page);
            if (page.ViewModel != null)
            {
                page.ViewModel.OnNavigatedTo(parameters);
            }
        }
    }
}
