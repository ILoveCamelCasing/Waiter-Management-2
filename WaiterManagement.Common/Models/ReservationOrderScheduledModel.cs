using System.Collections;
using System.Collections.Generic;

namespace WaiterManagement.Common.Models
{
	public class ReservationOrderScheduledModel
	{
		public string UnlockCode { get; set; }

		public IEnumerable<OrderingMenuItem> MenuItems { get; set; }
	}
}
