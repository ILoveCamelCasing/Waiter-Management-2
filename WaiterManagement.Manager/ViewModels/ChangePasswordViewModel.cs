using System;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels
{
	public class ChangePasswordViewModel: ViewModelBase
	{
		#region Dependencies

		private readonly ICommandBus _commandBus;

		#endregion

		#region Private fields

		private int _entityId;
		private Type _entityType;
		private string _userPassword;

		#endregion

		#region Public properties

		public string Login { get; private set; }

		public string UserPassword
		{
			get { return _userPassword; }
			set
			{
				_userPassword = value;
				NotifyOfPropertyChange(() => CanSave);
			}
		}

		public bool CanSave
		{
			get
			{
				return !string.IsNullOrEmpty(UserPassword);
			}
		}

		#endregion

		#region Constructor

		public ChangePasswordViewModel(IViewModelResolver viewModelResolver, ICommandBus commandBus)
			: base(viewModelResolver)
		{
			_commandBus = commandBus;
		}

		#endregion

		#region Public methods

		public void Save()
		{
			_commandBus.SendCommand(new ChangePasswordCommand(){EntityId = _entityId, EntityType = _entityType, Password = UserPassword});
			Close();
		}

		public void Initialize(int entityId , string login, Type type)
		{
			_entityId = entityId;
			_entityType = type;

			Login = login;
		}

		#endregion
	}
}