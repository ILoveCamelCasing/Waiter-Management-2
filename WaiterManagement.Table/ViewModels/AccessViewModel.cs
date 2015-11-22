using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Table.ViewModels
{
	public class AccessViewModel : ViewModelBase
	{
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

		public AccessViewModel(IViewModelResolver viewModelResolver) : base(viewModelResolver)
		{
		}

		public void LoginToServer()
		{
			
		}
	}
}