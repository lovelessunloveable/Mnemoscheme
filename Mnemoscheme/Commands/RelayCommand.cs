using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mnemoscheme.Commands
{
    internal class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action OnExecuteCommand;

        public RelayCommand(Action Command) 
        {
            OnExecuteCommand = Command;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            OnExecuteCommand?.Invoke();
        }
    }
}
