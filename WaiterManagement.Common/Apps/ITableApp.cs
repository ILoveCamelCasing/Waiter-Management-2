using WaiterManagement.Common.Models;

namespace WaiterManagement.Common.Apps
{
	public interface ITableApp
	{
		void NotifyTable(string message);
		void NotifyOrderItemStateChanged(OrderItemState state);
		void SendOrderId(int id);
	}
}