using System.Web.Mvc;
using WaiterManagement.Web.Infrastructure.Authentication;
using WaiterManagement.Web.Models;

namespace WaiterManagement.Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAuthProvider _authProvider;

		public AccountController(IAuthProvider authProvider)
		{
			_authProvider = authProvider;
		}

		public ActionResult Summary()
		{
			return PartialView(_authProvider);
		}

		public ActionResult LogIn()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogIn(LogInUser logInUser)
		{
			if (ModelState.IsValid)
			{
				_authProvider.LogIn(logInUser);
				return RedirectToAction("Index", "Home");
			}

			return View(logInUser);
		}

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(RegisterUser registerUser)
		{
			if (ModelState.IsValid)
			{
				_authProvider.Register(registerUser);
				return RedirectToAction("Index", "Home");
			}

			return View(registerUser);
		}
	}
}