using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFUI.Utility
	{
	public class RelayCommand<T> : ICommand
	{
		private readonly Action<T> _execute = null;
		private readonly Func<T, bool> _canExecute = null;

		public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute ?? (x => true);
		}

		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}

		public bool CanExecute(object parameter) => _canExecute((T)parameter);

		public void Execute(object parameter) => _execute((T)parameter);
	}

	//Infers type object from inheritance, calls base methods without need for object parameters on passed methods, null gives true for can execute so func not needed
	public class RelayCommand : RelayCommand<object>
	{
		public RelayCommand(Action execute)
			: base(x => execute()) { }

		public RelayCommand(Action execute, Func<bool> canExecute)
			: base(x => execute(), x => canExecute()) { }
	}
}
