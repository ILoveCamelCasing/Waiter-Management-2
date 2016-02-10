using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WaiterManagement.Web.Infrastructure.Authentication;
using WaiterManagement.Web.Infrastructure.ServerProviders;
using WaiterManagement.Web.Models;

namespace WaiterManagement.Web.Infrastructure.Ordering
{
	public interface ICartProvider
	{
		Cart GetCartForLoggedUser();
		Cart AddElementTuCurrentOrder(int elementId);
		void Checkout(DateTime date);
	}

	public class CartProvider : ICartProvider
	{
		private readonly IAuthProvider _authProvider;
		private readonly IMenuProvider _menuProvider;

		public CartProvider(IAuthProvider authProvider, IMenuProvider menuProvider)
		{
			_authProvider = authProvider;
			_menuProvider = menuProvider;
		}

		public Cart GetCartForLoggedUser()
		{
			if(!_authProvider.IsLogged)
				throw new InvalidOperationException("User is not logged in.");

			var userCartSessoinKey = GetUserCartSessionKey();
			var cart = (Cart) HttpContext.Current.Session[userCartSessoinKey] ?? CreateNewCart(userCartSessoinKey);

			return cart;
		}

		public Cart AddElementTuCurrentOrder(int elementId)
		{
			var cart = GetCartForLoggedUser();

			var element = cart.CartItems.FirstOrDefault(x => x.ItemId == elementId);

			if (element != null)
			{
				element.Quantity++;
			}
			else
			{
				var newElement = _menuProvider.GetMenu().First(x => x.MenuItemId == elementId);
				cart.Add(elementId,newElement.Title,newElement.Price);
			}

			return cart;
		}

		public void Checkout(DateTime date)
		{
			CheckCheckout(date);

			using (var client = new HttpClient())
			{
				try
				{
					client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServerPath"]);
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					client.DefaultRequestHeaders.Add("login", _authProvider.Username);
					client.DefaultRequestHeaders.Add("token", _authProvider.Token);
					dynamic myObject = new JObject();
					myObject.login = _authProvider.Username;
					myObject.orderDate = date;
					myObject.items = new JArray(
						GetCartForLoggedUser().CartItems.Select(x =>
						{
							dynamic itemObject = new JObject();
							itemObject.itemId = x.ItemId;
							itemObject.quantity = x.Quantity;

							return itemObject;
						}).ToArray());

					var result = client.PostAsync(ConfigurationManager.AppSettings["OrderPath"], new StringContent(JsonConvert.SerializeObject(myObject).ToString(), Encoding.UTF8, "application/json")).Result;
					if (result.StatusCode != HttpStatusCode.OK)
					{
						throw new Exception($"Server error. Status: {result.StatusCode}. Message: {result.RequestMessage}");
					}

					CreateNewCart(GetUserCartSessionKey());
				}
				catch (HttpRequestException e)
				{
					throw new Exception("Internal server error.");
				}
			}
		}

		private void CheckCheckout(DateTime date)
		{
			if(date.Hour < 10 || date.Hour > 22 || (date.Hour == 22 && date.Minute > 0))
				throw new InvalidOperationException("Reservation should be set between 10-22");

			if(GetCartForLoggedUser().CartItems.Count == 0)
				throw new InvalidOperationException("Set menu items to your checkout.");
		}

		private string GetUserCartSessionKey()
		{
			var userCartSessoinKey = $"{_authProvider.Username}_Cart";
			return userCartSessoinKey;
		}

		private static Cart CreateNewCart(string userCartSessoinKey)
		{
			Cart cart;
			cart = new Cart();
			HttpContext.Current.Session[userCartSessoinKey] = cart;
			return cart;
		}
	}
}