using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WaiterManagement.BLL.Commands.Concrete;
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
    private IUnitOfWork _unitOfWork; //TODO: DI (?), TODO: Jakiś rozszerzony interfejs dostępu do danych (zarówno widoki jak i tabele, zwracanie IQueryable ?
    private IViewProvider _viewProvider;
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
      //TODO Walidacja danych na wejściu?

      //Sprawdzenie istnienie kelnera w bazie danych
      var waiter = _viewProvider.Get<WaiterView>().FirstOrDefault(w => w.Login == login);
      if (waiter == null)
        return BadRequest(String.Format("No waiter for login {0}.", login));

      var waiterUser = _viewProvider.Get<UserView>().FirstOrDefault(u => u.UserId == waiter.WaiterGuid);

      //Walidacja hasła
      if (!HashUtility.ValidatePassword(firstHash, waiterUser.SecondHash))
        return BadRequest("Wrong password."); //??

      //Generowanie i zapis tokena 
      var userGuid = Guid.NewGuid();
      var activeUser = new ActiveUser() { UserId = waiterUser.CommonId, UserToken = userGuid, TokenCreation = DateTime.Now };
      _unitOfWork.Add(activeUser);

      //Zwracanie tokena
      return Ok(userGuid);
		}

    [ResponseType(typeof(Guid))]
    [HttpGet]
    public IHttpActionResult LoginTable(string login, string firstHash)
    {
      //TODO Walidacja danych na wejściu?

      //Sprawdzenie istnienie stołu w bazie danych
      var table = _viewProvider.Get<TableView>().FirstOrDefault(t => t.Login == login);
      if (table == null)
        return BadRequest(String.Format("No table for login {0}.", login));

      var tableUser = _viewProvider.Get<UserView>().FirstOrDefault(u => u.UserId == table.TableGuid);

      //Walidacja hasła
      if (!HashUtility.ValidatePassword(firstHash, tableUser.SecondHash))
        return BadRequest("Wrong password."); //??

      //Generowanie i zapis tokena 
      var userGuid = Guid.NewGuid();
      var activeUser = new ActiveUser() { UserId = tableUser.CommonId, UserToken = userGuid, TokenCreation = DateTime.Now };
      _unitOfWork.Add(activeUser);

      //Zwracanie tokena
      return Ok(userGuid);
    }

    [ResponseType(typeof(bool))] //TODO Zwrócić coś innego
    [HttpGet]
    public IHttpActionResult AddWaiter(AddWaiterCommand addWaiterCommand)
    {
      //TODO Walidacja na wejściu
      //TODO otwarcie transakcji (?)

      //Sprawdzenie istnienia kelnera w bazie danych (Sprawdzanie unikalności na podstawie loginu)
      var waiterView = _viewProvider.Get<WaiterView>().FirstOrDefault(w => w.Login == addWaiterCommand.Login);
      if (waiterView != null)
        return BadRequest(String.Format("Waiter with login {0} has already been added.", addWaiterCommand.Login));

      //Dodanie kelnera do tabeli Waiter
      var waiter = _unitOfWork.Add(new Waiter() { Login = addWaiterCommand.Login, FirstName = addWaiterCommand.FirstName, LastName = addWaiterCommand.LastName });

      //Dodanie kelnera do tabeli Users
      var secondHash = HashUtility.CreateSecondHash(addWaiterCommand.FirstHash);
      _unitOfWork.Add(new User() { UserId = waiter.CommonId, SecondHash = secondHash });

      return Ok(true);
    }

    [ResponseType(typeof(bool))] //TODO Zwrócić coś innego
    [HttpGet]
    public IHttpActionResult AddTable(AddTableCommand addTableCommand)
    {
      //TODO Walidacja na wejściu
      //TODO otwarcie transakcji (?)

      //Sprawdzenie istnienia stołu w bazie danych (Sprawdzanie unikalności na podstawie loginu)
      var tableView = _viewProvider.Get<TableView>().FirstOrDefault(t => t.Login == addTableCommand.Login);
      if (tableView != null)
        return BadRequest(String.Format("Table with login {0} has already been added.", addTableCommand.Login));

      //Dodanie stołu do tabeli Table
      var table = _unitOfWork.Add(new Table() { Login = addTableCommand.Login, Title = addTableCommand.Title, Description = addTableCommand.Description });

      //Dodanie stołu do tabeli Users
      var secondHash = HashUtility.CreateSecondHash(addTableCommand.FirstHash);
      _unitOfWork.Add(new User() { UserId = table.CommonId, SecondHash = secondHash });

      return Ok(true);
    }
    #endregion
  }
}
