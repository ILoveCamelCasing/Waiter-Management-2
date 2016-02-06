using System.Collections.Generic;
using WaiterManagement.BLL.Events.Base;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Events.Concrete.Service
{
	public class AddedWebOrder : IEvent
	{
		public ReservationOrder Order { get; set; }
		public List<ReservationMenuItemQuantity> MenuItems { get; set; }
	}
}