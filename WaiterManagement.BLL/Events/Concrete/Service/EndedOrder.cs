using WaiterManagement.BLL.Events.Base;

namespace WaiterManagement.BLL.Events.Concrete.Service
{
	public class EndedOrder : IEvent
	{
		public int OrderId { get; set; }
		public bool OrderCancelled { get; set; }
		public string OrderCancelledReason { get; set; }
		public string TableLogin { get; set; }
	}
}
