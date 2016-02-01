using System;
using System.Web.Mvc;
using WaiterManagement.Web.Infrastructure.Authentication;
using WaiterManagement.Web.Infrastructure.Ordering;

namespace WaiterManagement.Web.Controllers
{
	public class OrderController : Controller
	{
		private readonly ICartProvider _cartProvider;
		private readonly IAuthProvider _authProvider;

		public OrderController(ICartProvider cartProvider, IAuthProvider authProvider)
		{
			_cartProvider = cartProvider;
			_authProvider = authProvider;
		}

		public ActionResult Summary()
		{
			if(_authProvider.IsLogged)
				return PartialView(_cartProvider.GetCartForLoggedUser());

			return new EmptyResult();
		}

		public ActionResult AddElement(int elementId)
		{
			_cartProvider.AddElementTuCurrentOrder(elementId);
			return new EmptyResult();
		}

		public ActionResult Checkout()
		{
			if(HttpContext.Request.HttpMethod == "GET")
				return PartialView(_cartProvider.GetCartForLoggedUser());

			_cartProvider.Checkout(DateTime.Now);

			return RedirectToAction("Index", "Home");
		}
	}
}