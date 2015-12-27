using System.Collections.Generic;

namespace WaiterManagement.Common.Models
{
	public class AcceptedOrderCurrentStateModel
	{
		public int OrderId { get; set; }
		public IEnumerable<AcceptedOrderMenuItemQuantity> MenuItems { get; set; }
	}
}
