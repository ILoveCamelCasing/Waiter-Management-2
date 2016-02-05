using WaiterManagement.BLL.Events.Base;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Events.Concrete.Service
{
	public class ReservationOrderScheduled : IEvent
	{
		public Order Order { get; set; }
	}
}
