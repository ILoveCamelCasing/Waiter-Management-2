using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
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
		private readonly ICommandBus _commandBus;
		#endregion

		#region Constructors
		public AccountController(IUnitOfWork unitOfWork, IViewProvider viewProvider, ICommandBus commandBus)
		{
			if (unitOfWork == null)
				throw new ArgumentNullException(nameof(unitOfWork));
			if (viewProvider == null)
				throw new ArgumentNullException(nameof(viewProvider));
			if(commandBus == null)
				throw new ArgumentNullException(nameof(commandBus));

			_unitOfWork = unitOfWork;
			_viewProvider = viewProvider;
			_commandBus = commandBus;
		}
		#endregion

		#region POST Methods
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

		[ResponseType(typeof (Guid))]
		[HttpPost]
		public IHttpActionResult LoginWebClient([FromBody] LoginModel loginModel)
		{
			return Login<WebClientView>(loginModel);
		}

		[HttpPost]
		public IHttpActionResult RegisterWebClient([FromBody] RegisterWebClientModel client )
		{
			try
			{
				_commandBus.SendCommand(new AddWebClientCommand()
				{
					Login = client.Login,
					FirstHash = client.FirstHash,
					FirstName = client.FirstName,
					LastName = client.LastName,
					Phone = client.Phone,
					Mail = client.Mail
				});
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		#endregion

		#region Private methods
		private IHttpActionResult Login<T>([FromBody] LoginModel loginModel) where T : class, ILoginableView
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
			_unitOfWork.Commit();

			//Zwracanie tokena
			return Ok(userGuid);
		}
		#endregion
	}
}
