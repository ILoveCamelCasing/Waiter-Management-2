using System.Web.Mvc;
using WaiterManagement.Web.Infrastructure.Authentication;
using WaiterManagement.Web.Infrastructure.ServerProviders;
using WaiterManagement.Web.Models;

namespace WaiterManagement.Web.Controllers
{
    public class HomeController : Controller
    {
	    private readonly IMenuProvider _menuProvider;
	    private readonly IAuthProvider _authProvider;
	    //
        // GET: /Home/

	    public HomeController(IMenuProvider menuProvider, IAuthProvider authProvider)
	    {
		    _menuProvider = menuProvider;
		    _authProvider = authProvider;
	    }

	    public ActionResult Index()
        {
            return View();
        }

	    public ActionResult Menu()
	    {
		    return View(new MenuData() { MenuCategories = _menuProvider.GetCategories(), IsLoggedUser = _authProvider.IsLogged });
	    }

	    public ActionResult IndexInside()
	    {
		    return View();
	    }

	    public ActionResult Contact()
	    {
		    return View();
	    }

	    public ActionResult Info()
	    {
		    return View();
	    }

    }
}
