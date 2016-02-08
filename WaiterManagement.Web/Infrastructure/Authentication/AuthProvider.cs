﻿using System;
using System.IO;
using System.Web;
using WaiterManagement.Common.Security;
using WaiterManagement.Web.Helpers;
using WaiterManagement.Web.Models;

namespace WaiterManagement.Web.Infrastructure.Authentication
{
	class AuthProvider : IAuthProvider
	{
		private const string LoggedUserSessionValueName = "LoggedUser";
		private const string TokenSessionValueName = "Token";
		private const string LogInStatusSessionValueName = "LogInStatus";

		private readonly ILogInStrategy _logInStrategy;
		private readonly IRegisterWebUserStrategy _registerWebUserStrategy;

		public AuthProvider(ILogInStrategy logInStrategy, IRegisterWebUserStrategy registerWebUserStrategy)
		{
			_logInStrategy = logInStrategy;
			_registerWebUserStrategy = registerWebUserStrategy;
		}

		public bool IsLogged
			=>
				HttpContext.Current.Session[LogInStatusSessionValueName] != null &&
				(bool) HttpContext.Current.Session[LogInStatusSessionValueName];

		public string Username => HttpContext.Current.Session[LoggedUserSessionValueName] as string;
		public string Token => HttpContext.Current.Session[TokenSessionValueName] as string;

		public void LogIn(LogInUser logInUser)
		{
			var result = AsyncHelpers.RunSync(() => _logInStrategy.LogIn(logInUser.Username, logInUser.Password));
			switch (result.Result)
			{
				case LoginResultType.LoginOk:
					HttpContext.Current.Session[LogInStatusSessionValueName] = true;
					HttpContext.Current.Session[LoggedUserSessionValueName] = logInUser.Username;
					HttpContext.Current.Session[TokenSessionValueName] = result.Token;
					break;
				case LoginResultType.LoginFailed:
					throw new Exception("Wrong username or password.");
				case LoginResultType.ConnectionError:
					throw new Exception("There are some problms now. Please try later.");
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void Register(RegisterUser user)
		{
			_registerWebUserStrategy.RegisterUser(user);
		}

		public void LogOut()
		{
			HttpContext.Current.Session[LogInStatusSessionValueName] = false;
		}
	}
}