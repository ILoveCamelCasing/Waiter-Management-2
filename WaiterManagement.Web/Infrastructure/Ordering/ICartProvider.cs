using System;
using System.Linq;
using System.Web;
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

			var userCartSessoinKey = $"{_authProvider.Username}_Cart";
			var cart = (Cart) HttpContext.Current.Session[userCartSessoinKey];
			if (cart == null)
			{
				cart = new Cart();
				HttpContext.Current.Session[userCartSessoinKey] = cart;
			}

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
			throw new NotImplementedException();
		}
	}
}