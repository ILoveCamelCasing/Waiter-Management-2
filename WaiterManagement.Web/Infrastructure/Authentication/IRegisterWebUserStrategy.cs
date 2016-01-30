using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WaiterManagement.Common.Security;
using WaiterManagement.Web.Models;

namespace WaiterManagement.Web.Infrastructure.Authentication
{
	public interface IRegisterWebUserStrategy
	{
		void RegisterUser(RegisterUser user);
	}

	public class RegisterWebUserStrategy : IRegisterWebUserStrategy
	{
		#region Dependencies

		private readonly IPasswordManager _passwordManager;

		#endregion

		#region Constructor 

		public RegisterWebUserStrategy(IPasswordManager passwordManager)
		{
			_passwordManager = passwordManager;
		}

		#endregion

		public void RegisterUser(RegisterUser user)
		{
			using (var client = new HttpClient())
			{
				try
				{
					client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServerPath"]);
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					dynamic myObject = new JObject();
					myObject.login = user.Username;
					myObject.firstHash = _passwordManager.CreateFirstHash(user.Username, user.Password);
					myObject.firstName = user.FirstName;
					myObject.lastName = user.LastName;
					myObject.phone = user.Phone;
					myObject.mail = user.Mail;

					var result = client.PostAsync(ConfigurationManager.AppSettings["RegisterPath"], new StringContent(JsonConvert.SerializeObject(myObject).ToString(), Encoding.UTF8, "application/json")).Result;
					if (result.StatusCode != HttpStatusCode.OK)
					{
						throw new Exception($"Server error. Status: {result.StatusCode}. Message: {result.RequestMessage}");
					}
				}
				catch (HttpRequestException e)
				{
					throw new Exception("Internal server error.");
				}
			}
		}
	}
}