using WaiterManagement.Common.Security;
using WaiterManagement.Waiter.Connection;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Waiter.ViewModels
{
	public class AccessViewModel : ViewModelBase
	{
		#region Dependencies

		private readonly IAccessProvider _accessProvider;
		private readonly IWaiterConnectionProvider _waiterConnectionProvider;

		#endregion

		#region Private fields

		private string _login;
		private string _userPassword;

		#endregion

		#region Public properties

		public string Login
		{
			get { return _login; }
			set
			{
				_login = value;
				NotifyOfPropertyChange(() => CanLoginToServer);
			}
		}

		public string UserPassword
		{
			get { return _userPassword; }
			set
			{
				_userPassword = value;
				NotifyOfPropertyChange(() => CanLoginToServer);
			}
		}

		public bool CanLoginToServer
		{
			get
			{
				return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(UserPassword);
			}
		}

		#endregion

		public AccessViewModel(IViewModelResolver viewModelResolver, IAccessProvider accessProvider, IWaiterConnectionProvider waiterConnectionProvider)
			: base(viewModelResolver)
		{
			_accessProvider = accessProvider;
			_waiterConnectionProvider = waiterConnectionProvider;
		}

		public void LoginToServer()
		{
			if (_accessProvider.LogIn(Login, UserPassword))
			{
				_waiterConnectionProvider.Connect();
				Close();
				Get<OrdersViewModel>().ShowOn(ParentWindow);
			}
		}
	}
}