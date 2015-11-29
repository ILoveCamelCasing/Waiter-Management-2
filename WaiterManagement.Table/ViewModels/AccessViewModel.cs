using System;
using Microsoft.AspNet.SignalR.Client;
using WaiterManagement.Common.Security;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Table.ViewModels
{
	public class AccessViewModel : ViewModelBase
	{
		#region Dependencies

		private readonly IAccessProvider _accessProvider;

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

		public AccessViewModel(IViewModelResolver viewModelResolver, IAccessProvider accessProvider)
			: base(viewModelResolver)
		{
			_accessProvider = accessProvider;
		}

		public void LoginToServer()
		{
			if (_accessProvider.Login(Login, UserPassword))
			{
				Close();
			}
			//var hubConnection = new HubConnection("http://localhost:8080/");
			//var tableHubProxy = hubConnection.CreateHubProxy("tableHub");

			//tableHubProxy.On<Guid>("Login", token => this.Close());

			//hubConnection.Start().Wait();
			//tableHubProxy.Invoke("Login", Login, _passwordManager.CreateFirstHash(Login, UserPassword));
		}
	}
}