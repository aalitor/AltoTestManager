using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AltoTestManager
{
    /// <summary>
    /// DelegateCommand borrowed from
    /// http://www.wpftutorial.net/DelegateCommand.html
    /// </summary>
    public class DelegateCommand : ICommand
    {
        public Action CommandAction { get; set; }

        public DelegateCommand(Action commandAction)
        {
            this.CommandAction = commandAction;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            CommandAction();
        }
    }


    public class DelegateParameterCommand<T> : ICommand
    {
        public Action<T> CommandAction { get; set; }

        public DelegateParameterCommand(Action<T> commandAction)
        {
            this.CommandAction = commandAction;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            CommandAction((T)parameter);
        }

    }
}
