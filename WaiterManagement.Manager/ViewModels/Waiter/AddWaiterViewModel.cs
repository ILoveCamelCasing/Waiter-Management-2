using System;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Waiter
{
	[UseView("Waiter.WaiterView")]
	public class AddWaiterViewModel : ViewModelBase
	{
		#region Dependencies

		private readonly ICommandBus _commandBus;

		#endregion

		#region Private fields

		private string _login;
		private string _firstName;
		private string _lastName;
		private string _userPassword;

		#endregion

		#region Public Properties

		public string Login { get { return _login; } set { _login = value; NotifyOfPropertyChange(() => CanSave); } }
		public string FirstName { get { return _firstName; } set { _firstName = value; NotifyOfPropertyChange(() => CanSave); } }
		public string LastName { get { return _lastName; } set { _lastName = value; NotifyOfPropertyChange(() => CanSave); } }
		public string UserPassword { get { return _userPassword; } set { _userPassword = value; NotifyOfPropertyChange(() => CanSave); } }
		public bool CanSave
		{
			get
			{
				return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(UserPassword);
			}
		}
		#endregion

		#region Constructors
		public AddWaiterViewModel(IViewModelResolver viewModelResolver, ICommandBus commandBus)
			: base(viewModelResolver)
		{
			if (commandBus == null)
				throw new ArgumentNullException("commandBus");

			_commandBus = commandBus;
		}
		#endregion

		#region Public Methods
		public void Save()
		{
			_commandBus.SendCommand(new AddWaiterCommand() { Login = Login, FirstName = FirstName, LastName = LastName, Password = UserPassword });

			Close();
		}

		public void Cancel()
		{
			Close();
		}
		#endregion
	}
}
