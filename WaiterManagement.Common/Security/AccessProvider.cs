using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WaiterManagement.Common.Security
{
	public class AccessProvider : IAccessProvider
	{
		#region Dependencies

		private readonly IPasswordManager _passwordManager;

		#endregion

		#region Public properties

		public string Login { get; private set; }
		public string Token { get; private set; }

		#endregion

		public AccessProvider(IPasswordManager passwordManager)
		{
			_passwordManager = passwordManager;
		}

		public bool LogIn(string login, string password)
		{
			Login = login;

			
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServerPath"]);
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				dynamic myObject = new JObject();
				myObject.login = login;
				myObject.firstHash = _passwordManager.CreateFirstHash(login, password);

				var result = client.PostAsync(ConfigurationManager.AppSettings["LoginPath"], new StringContent(JsonConvert.SerializeObject(myObject).ToString(), Encoding.UTF8, "application/json")).Result;
				var resultString = result.Content.ReadAsStringAsync().Result.Replace("\"","");
				
				Guid guid;
				if (!Guid.TryParse(resultString, out guid))
					return false;

				Token = resultString;
				return true;
			}
		}
	}
}