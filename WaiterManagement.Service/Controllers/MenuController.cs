using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using WaiterManagement.Common.Views;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.Service.Controllers
{
  public class MenuController : ApiController
  {
    #region Private Fields
    private readonly IViewProvider _viewProvider;
    #endregion

    #region Constructors
    public MenuController(IViewProvider viewProvider)
    {
      if (viewProvider == null)
        throw new ArgumentNullException("viewProvider");

      _viewProvider = viewProvider;
    }
    #endregion

    #region GET Methods
    [ResponseType(typeof(IEnumerable<MenuItemView>))]
    [HttpGet]
    public IHttpActionResult GetMenu()
    {
      //Ze względu na to, że MenuItem mają już tytuł kategorii, wyciąganie właściwych kategorii może być zbędne. 
      //Na razie zwracam płaską kolekcję, do rozważenia jak to ma wyglądać docelowo

      ////Pobieranie MenuItemek
      //var menuItems = _viewProvider.Get<MenuItemView>();
      //var menuItemsDict = menuItems.GroupBy(mi => mi.CategoryId).ToDictionary(g => g.Key, g => g.ToList());

      ////Dociąganie odpowiednich kategorii
      //var categoryDict = _viewProvider.Get<CategoryView>().Where(c => menuItemsDict.Keys.Contains(c.CategoryId)).ToDictionary(c => c.CategoryId);

      ////Budowanie końcowego słownika
      //return Ok(menuItemsDict.ToDictionary(kv => categoryDict[kv.Key], kv => kv.Value));

      return Ok(_viewProvider.Get<MenuItemView>().AsEnumerable());
    }
    #endregion
  }
}
