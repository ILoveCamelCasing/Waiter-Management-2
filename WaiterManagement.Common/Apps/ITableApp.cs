using WaiterManagement.Common.Models;

namespace WaiterManagement.Common.Apps
{
	public interface ITableApp
	{
		void NotifyTable(string message);
		void NotifyOrderItemStateChanged(OrderItemState state);
		void NotifyOrderEnded(EndOrderModel endedOrder);
		void SendOrderId(int id);
		void LockTable(ReservationOrderScheduledModel orderScheduled);
	}
}