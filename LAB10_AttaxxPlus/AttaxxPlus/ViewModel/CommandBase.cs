using System;
using System.Windows.Input;

namespace AttaxxPlus.ViewModel
{
    // EVIP: default implementation for an interface
    public abstract class CommandBase : ICommand
    {
        // EVIP: temporarily disable a warning
#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning restore 67
        public bool CanExecute(object parameter) => true;
        public abstract void Execute(object parameter);
    }
}
