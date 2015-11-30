using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WaiterManagement.Common.Security;

namespace WaiterManagement.Table
{
	public class AccessProvider : IAccessProvider
	{
		#region Dependencies

		private readonly IPasswordManager _passwordManager;

		#endregion

		#region Public properties

		public string TableLogin { get; private set; }

		#endregion

		public AccessProvider(IPasswordManager passwordManager)
		{
			_passwordManager = passwordManager;
		}

		public bool Login(string login, string password)
		{
			TableLogin = login;

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServerPath"]);
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				dynamic myObject = new JObject();
				myObject.login = login;
				myObject.firstHash = _passwordManager.CreateFirstHash(login, password);

				var result = client.PostAsync("/api/Account/LoginTable", new StringContent(JsonConvert.SerializeObject(myObject).ToString(), Encoding.UTF8, "application/json")).Result;
				var resultContent = result.Content.ReadAsStringAsync().Result;

				return true;
			}
		}
	}
}