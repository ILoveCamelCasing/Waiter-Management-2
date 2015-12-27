using WaiterManagement.Common.Models;

namespace WaiterManagement.Common.Apps
{
	public interface IWaiterApp
	{
		void NewOrderMade(OrderModel order);
		void AcceptedOrderInfoUpdated(AcceptedOrderCurrentStateModel acceptedOrder);
		void CallWaiter(string tableLogin);
	}
}