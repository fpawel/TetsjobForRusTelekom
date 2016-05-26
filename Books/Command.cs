using System;
using System.Windows.Input;

namespace Books
{
    public class Command : ICommand
    {
        private readonly Action action;

        public Command(Action a)
        {
            action = a;
        }
        bool ICommand.CanExecute(object parameter)
        {
            return true;
        }
        void ICommand.Execute(object parameter)
        {
            action();
        }

        public event EventHandler CanExecuteChanged;

        public static ICommand create(Action a)
        {
            return new Command(a);
        }
    }
}
