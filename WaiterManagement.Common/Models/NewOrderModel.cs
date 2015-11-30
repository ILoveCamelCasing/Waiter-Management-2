using System.Collections.Generic;

namespace WaiterManagement.Common.Models
{
	public class NewOrderModel
	{
		public string TableLogin { get; set; }
		public IEnumerable<OrderingMenuItem> OrderingMenuItems { get; set; }
	}
}