using WaiterManagement.BLL.Events.Base;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Events.Concrete.Service
{
	public class ReservationOrderScheduled : IEvent
	{
		public string UnlockCode { get; set; }
		public Order Order { get; set; }
	}
}
