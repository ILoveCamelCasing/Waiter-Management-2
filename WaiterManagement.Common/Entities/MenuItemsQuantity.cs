using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
	public class MenuItemsQuantity : NonVersionableEntity
	{
		public Order Order { get; set; }
		public MenuItem Item { get; set; }
		public int Quantity { get; set; }
		public bool Ready { get; set; }
	}
}