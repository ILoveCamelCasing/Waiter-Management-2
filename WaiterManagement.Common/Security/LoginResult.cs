namespace WaiterManagement.Common.Security
{
	public class LoginResult
	{
		public LoginResultType Result { get; private set; }
		public string Token { get; private set; }

		private LoginResult(LoginResultType result, string token)
		{
			Result = result;
			Token = token;
		}

		public static LoginResult GetLogged(string token)
		{
			return new LoginResult(LoginResultType.LoginOk, token);
		}

		public static LoginResult GetFailed()
		{
			return new LoginResult(LoginResultType.LoginFailed, null);
		}

		public static LoginResult GetConnectionErrorFailed()
		{
			return new LoginResult(LoginResultType.LoginFailed, null);
		}
	}
}