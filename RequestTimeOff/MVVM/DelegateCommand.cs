using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RequestTimeOff.MVVM
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;
        private readonly Func<bool> _canExecute;
        public DelegateCommand(Action action, Func<bool> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute;

        }
#pragma warning disable CS0067 // The event 'DelegateCommand.CanExecuteChanged' is never used
        public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067 // The event 'DelegateCommand.CanExecuteChanged' is never used

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) 
            { 
                return true;
            }
            return _canExecute();
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _action;
        private readonly Func<T, bool> _canExecute;
        public DelegateCommand(Action<T> action, Func<T, bool> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute;
        }


#pragma warning disable CS0067 // The event 'DelegateCommand.CanExecuteChanged' is never used
        public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067 // The event 'DelegateCommand.CanExecuteChanged' is never used

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute((T)parameter);
        }

        public void Execute(object parameter)  
        {
            _action((T)parameter);
        }
    }
}
