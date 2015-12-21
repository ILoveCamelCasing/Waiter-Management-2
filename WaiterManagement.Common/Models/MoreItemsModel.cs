using System.Collections.Generic;

namespace WaiterManagement.Common.Models
{
	public class MoreItemsModel
	{
		public int OrderId { get; set; }
		public IEnumerable<OrderingMenuItem> OrderingMenuItems { get; set; } 
	}
}