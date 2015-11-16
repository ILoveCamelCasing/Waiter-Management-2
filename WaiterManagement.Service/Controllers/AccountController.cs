using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;
using WaiterManagement.Common.Security;
using WaiterManagement.Common.Views;
using WaiterManagement.Common.Views.Abstract;

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

		#region GET Methods

		[ResponseType(typeof(Guid))]
		[HttpGet]
		public IHttpActionResult LoginWaiter(string login, string firstHash)
		{
			return Login<WaiterView>(login, firstHash);
		}

		[ResponseType(typeof(Guid))]
		[HttpGet]
		public IHttpActionResult LoginTable(string login, string firstHash)
		{
			return Login<TableView>(login, firstHash);
		}

		#endregion

		#region Private methods

		private IHttpActionResult Login<T>(string login, string firstHash) where T : class, ILoginableView
		{
			//Sprawdzenie istnienie użytkownik w bazie danych
			var user = _viewProvider.Get<T>().FirstOrDefault(w => w.Login == login);
			if (user == null)
				return BadRequest(String.Format("No user for login {0}.", login));

			//Walidacja hasła
			if (!HashUtility.ValidatePassword(firstHash, user.SecondHash))
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
