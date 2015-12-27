using WaiterManagement.BLL.Events.Base;

namespace WaiterManagement.BLL.Events.Concrete.Service
{
	public class AcceptedOrder : IEvent
	{
		public int OrderId { get; set; }
		public string WaiterLogin { get; set; }
	}
}
