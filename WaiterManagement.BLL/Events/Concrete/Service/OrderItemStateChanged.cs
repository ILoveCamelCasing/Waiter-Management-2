using WaiterManagement.BLL.Events.Base;
using WaiterManagement.Common.Models;

namespace WaiterManagement.BLL.Events.Concrete.Service
{
	public class OrderItemStateChanged : IEvent
	{
		public int OrderId { get; set; }
		public string TableLogin { get; set; }
		public OrderItemState OrderItemState;
	}
}
