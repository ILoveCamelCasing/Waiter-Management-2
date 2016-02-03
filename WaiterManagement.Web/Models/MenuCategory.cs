namespace WaiterManagement.Web.Models
{
	public class MenuCategory
	{
		public string CategoryName { get; private set; }
		public int CategoryId { get; private set; }

		public MenuCategory(int categoryId, string categoryName)
		{
			CategoryId = categoryId;
			CategoryName = categoryName;
		}
	}
}