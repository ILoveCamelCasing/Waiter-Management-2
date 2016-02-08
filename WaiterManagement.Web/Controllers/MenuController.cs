using System.Linq;
using System.Web.Mvc;
using WaiterManagement.Web.Infrastructure.ServerProviders;

namespace WaiterManagement.Web.Controllers
{
    public class MenuController : Controller
    {
	    private readonly IMenuProvider _menuProvider;

	    public MenuController(IMenuProvider menuProvider)
	    {
		    _menuProvider = menuProvider;
	    }

	    //
        // GET: /Menu/

        public ActionResult Index()
        {
			return View(_menuProvider.GetMenu());
        }

	    public ActionResult Category(int categoryId)
	    {
		    return View("Index", _menuProvider.GetMenu().Where(x => x.CategoryId == categoryId));
	    }


	}
}
