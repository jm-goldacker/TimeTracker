using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyTime.ViewModels
{
#pragma warning disable CS8612 // Die NULL-Zulässigkeit von Verweistypen im Typ entspricht nicht dem implizit implementierten Member.
#pragma warning disable CS8767 // Die NULL-Zulässigkeit von Verweistypen im Typ des Parameters entspricht (möglicherweise aufgrund von Attributen für die NULL-Zulässigkeit) nicht dem implizit implementierten Member.
    public class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool>? _canExecute;


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value;}
        }

        public RelayCommand(Action<object> execute, Func<object, bool>? canExectue = null)
        {
            _execute = execute;
            _canExecute = canExectue;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
