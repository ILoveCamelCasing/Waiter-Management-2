using System.Threading.Tasks;

namespace WaiterManagement.Common.Security
{
	public class AccessProvider : IAccessProvider
	{
		#region Dependencies

		private readonly ILogInStrategy _logInStrategy;

		#endregion

		#region Public properties

		public string Login { get; private set; }
		public string Token { get; private set; }

		#endregion

		public AccessProvider(ILogInStrategy logInStrategy)
		{
			_logInStrategy = logInStrategy;
		}

		public async Task<LoginResultType> LogIn(string login, string password)
		{
			Login = login;

			var result = await _logInStrategy.LogIn(login, password);

			Token = result.Token;

			return result.Result;
		}
	}
}