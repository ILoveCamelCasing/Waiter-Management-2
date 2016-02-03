using System.Collections.Generic;

namespace WaiterManagement.Web.Models
{
	public class MenuData
	{
		public List<MenuCategory> MenuCategories { get; set; }
		public bool IsLoggedUser { get; set; }
	}
}