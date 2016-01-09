namespace WaiterManagement.Common.Models
{
	public class ChangeOrderItemStateModel
	{
		public int OrderId { get; set; }
		public int MenuItemQuantityId { get; set; }
		public bool Ready { get; set; }
	}
}
