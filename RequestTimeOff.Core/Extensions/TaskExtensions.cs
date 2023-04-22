using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleToAttribute("RequestTimeOff.Specflow")]
namespace RequestTimeOff.Extensions
{
    public static class TaskExtensions
    {
        public static async void Await(this Task task, Action<Exception> exceptionMethod, Action continueWith = null)
        {
            try
            {
                await task;
            } catch (Exception ex)
            {
                exceptionMethod.Invoke(ex);
            }
            continueWith?.Invoke();
        }
        public static async void Await<T>(this Task<T> task, Action<Exception> exceptionMethod, Action continueWith = null)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                exceptionMethod.Invoke(ex);
            }
            continueWith?.Invoke();
        }
    }
}
