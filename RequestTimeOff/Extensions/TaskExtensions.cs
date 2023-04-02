using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
