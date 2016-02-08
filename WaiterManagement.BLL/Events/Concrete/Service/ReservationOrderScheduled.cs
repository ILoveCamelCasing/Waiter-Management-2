using System.Collections;
using System.Collections.Generic;
using WaiterManagement.BLL.Events.Base;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Events.Concrete.Service
{
	public class ReservationOrderScheduled : IEvent
	{
		public string UnlockCode { get; set; }
		public string TableLogin { get; set; }
		public IEnumerable<ReservationMenuItemQuantity> MenuItems { get; set; }
	}
}
