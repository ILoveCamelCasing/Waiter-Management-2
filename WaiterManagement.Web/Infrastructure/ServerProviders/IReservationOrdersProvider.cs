using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using WaiterManagement.Common.Views;
using WaiterManagement.Web.Infrastructure.Authentication;

namespace WaiterManagement.Web.Infrastructure.ServerProviders
{
	public interface IReservationOrdersProvider
	{
		List<ReservationView> GetReservations();
	}

	public class ReservationOrdersProvider : IReservationOrdersProvider
	{
		private IAuthProvider _authProvider;

		public ReservationOrdersProvider(IAuthProvider authProvider)
		{
			_authProvider = authProvider;
		}

		public List<ReservationView> GetReservations()
		{
			var elements = new List<ReservationView>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServerPath"]);
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Add("login", _authProvider.Username);
				client.DefaultRequestHeaders.Add("token", _authProvider.Token);
				client.GetAsync("api/order/userall").ContinueWith(taskResponse =>
				{
					var response = taskResponse.Result;
					var jsonString = response.Content.ReadAsStringAsync();
					jsonString.Wait();
					elements = JsonConvert.DeserializeObject<List<ReservationView>>(jsonString.Result);
				}).Wait();
			}

			return elements;
		}
	}
}