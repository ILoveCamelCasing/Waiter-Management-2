using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
	public class ReservationMenuItemQuantity : NonVersionableEntity
	{
		public ReservationOrder ReservationOrder { get; set; }
		public MenuItem Item { get; set; }
		public int Quantity { get; set; }
	}
}