using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WaiterManagement.Common.Security;

namespace WaiterManagement.Table
{
	public class AccessProvider : IAccessProvider
	{
		private readonly IPasswordManager _passwordManager;

		public AccessProvider(IPasswordManager passwordManager)
		{
			_passwordManager = passwordManager;
		}

		public bool Login(string login, string password)
		{
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