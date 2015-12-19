﻿using System.Windows;
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
		private Visibility _wrongUsernameOrPassword;
		private bool _isBusy;

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

		public Visibility WrongUsernameOrPassword
		{
			get
			{
				return _wrongUsernameOrPassword;
			}
			set
			{
				_wrongUsernameOrPassword = value;
				NotifyOfPropertyChange(() => WrongUsernameOrPassword);
			}
		}

		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}
			set
			{
				_isBusy = value;
				NotifyOfPropertyChange(() => IsBusy);
			}
		}

		#endregion

		public AccessViewModel(IViewModelResolver viewModelResolver, IAccessProvider accessProvider)
			: base(viewModelResolver)
		{
			_accessProvider = accessProvider;
			_wrongUsernameOrPassword = Visibility.Hidden;
		}

		public async void LoginToServer()
		{
			WrongUsernameOrPassword = Visibility.Hidden;

			IsBusy = true;
			var loginSucceeded = await _accessProvider.LogIn(Login, UserPassword);
			IsBusy = false;

			if (loginSucceeded)
			{
				Close();
				Get<OrderViewModel>().ShowOn(ParentWindow);
			}
			else
				WrongUsernameOrPassword = Visibility.Visible;
		}
	}
}