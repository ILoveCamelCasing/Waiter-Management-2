using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;
using WaiterManagement.Common.Security;
using WaiterManagement.Common.Views;
using WaiterManagement.Common.Views.Abstract;
using WaiterManagement.Service.Models;

namespace WaiterManagement.Service.Controllers
{
	public class AccountController : ApiController
	{
		#region Private Fields

		private readonly IUnitOfWork _unitOfWork; //TODO: DI (?), TODO: Jakiś rozszerzony interfejs dostępu do danych (zarówno widoki jak i tabele, zwracanie IQueryable ?
		private readonly IViewProvider _viewProvider;

		#endregion

		#region Constructors

		public AccountController(IUnitOfWork unitOfWork, IViewProvider viewProvider)
		{
			if (unitOfWork == null)
				throw new ArgumentNullException("unitOfWork");
			if (viewProvider == null)
				throw new ArgumentNullException("viewProvider");

			_unitOfWork = unitOfWork;
			_viewProvider = viewProvider;
		}

		#endregion

		#region Post Methods

		[ResponseType(typeof(Guid))]
		[HttpPost]
		public IHttpActionResult LoginWaiter([FromBody] LoginModel loginModel)
		{
			return Login<WaiterView>(loginModel);
		}

		[ResponseType(typeof(Guid))]
		[HttpPost]
		public IHttpActionResult LoginTable([FromBody] LoginModel loginModel)
		{
			return Login<TableView>(loginModel);
		}

		#endregion

		#region Private methods

		private IHttpActionResult Login<T>(LoginModel loginModel) where T : class, ILoginableView
		{
			//Sprawdzenie istnienie użytkownik w bazie danych
			var user = _viewProvider.Get<T>().FirstOrDefault(w => w.Login == loginModel.Login);
			if (user == null)
				return BadRequest(String.Format("No user for login {0}.", loginModel.Login));

			//Walidacja hasła
			if (!HashUtility.ValidatePassword(loginModel.FirstHash, user.SecondHash))
				return BadRequest("Wrong password."); //??

			//Generowanie i zapis tokena 
			var userGuid = Guid.NewGuid();
			var activeUser = new ActiveUser() { UserId = user.UserId, UserToken = userGuid, TokenCreation = DateTime.Now };
			_unitOfWork.Add(activeUser);

			//Zwracanie tokena
			return Ok(userGuid);
		}

		#endregion
	}
}
