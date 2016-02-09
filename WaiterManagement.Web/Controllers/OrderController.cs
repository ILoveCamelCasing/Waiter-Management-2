using System;
using System.Web.Mvc;
using WaiterManagement.Web.Infrastructure.Authentication;
using WaiterManagement.Web.Infrastructure.Ordering;
using WaiterManagement.Web.Infrastructure.ServerProviders;

namespace WaiterManagement.Web.Controllers
{
	public class OrderController : Controller
	{
		private readonly ICartProvider _cartProvider;
		private readonly IAuthProvider _authProvider;
		private readonly IReservationOrdersProvider _reservationOrdersProvider;

		public OrderController(ICartProvider cartProvider, IAuthProvider authProvider, IReservationOrdersProvider reservationOrdersProvider)
		{
			_cartProvider = cartProvider;
			_authProvider = authProvider;
			_reservationOrdersProvider = reservationOrdersProvider;
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
			return PartialView(_cartProvider.GetCartForLoggedUser());
		}

		[HttpPost]
		public ActionResult Checkout(DateTime date)
		{
			try
			{
				_cartProvider.Checkout(date);
			}
			catch (Exception ex)
			{
				Response.StatusCode = 400;
				return View("SmallError",(object)ex.Message);
			}

			return RedirectToAction("Index", "Home");
		}

		public ActionResult All()
		{
			return PartialView(_reservationOrdersProvider.GetReservations());
		}
	}
}