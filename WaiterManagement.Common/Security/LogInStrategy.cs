using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WaiterManagement.Common.Security
{
	public interface ILogInStrategy
	{
		Task<LoginResult> LogIn(string login, string password);
	}

	public class LogInStrategy : ILogInStrategy
	{
		#region Dependencies

		private readonly IPasswordManager _passwordManager;

		#endregion

		#region Constructor 

		public LogInStrategy(IPasswordManager passwordManager)
		{
			_passwordManager = passwordManager;
		}

		#endregion

		public async Task<LoginResult> LogIn(string login, string password)
		{
			using (var client = new HttpClient())
			{
				try
				{
					client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServerPath"]);
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					dynamic myObject = new JObject();
					myObject.login = login;
					myObject.firstHash = _passwordManager.CreateFirstHash(login, password);

					var result = await client.PostAsync(ConfigurationManager.AppSettings["LoginPath"], new StringContent(JsonConvert.SerializeObject(myObject).ToString(), Encoding.UTF8, "application/json"));
					var resultString = (await result.Content.ReadAsStringAsync()).Replace("\"", "");

					Guid guid;
					if (!Guid.TryParse(resultString, out guid))
						return LoginResult.GetFailed();
					
					return LoginResult.GetLogged(resultString);
				}
				catch (HttpRequestException e)
				{
					//TODO: Zalogować wyjątek
					return LoginResult.GetConnectionErrorFailed();
				}
			}
		}
	}
}