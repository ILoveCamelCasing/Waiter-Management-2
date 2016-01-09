namespace WaiterManagement.Common.Models
{
	public class OrderItemState //TODO: Klasa bardzo podobna do OrderingMenuItem, może scalić do jednej klasy?
	{
		public int MenuItemId { get; set; }
		public int Quantity { get; set; }

		public bool Ready { get; set; }
	}
}
