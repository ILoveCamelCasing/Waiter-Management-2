using WaiterManagement.Common.Models;

namespace WaiterManagement.Common.Apps
{
	public interface IWaiterApp
	{
		void NewOrder(OrderModel order);
		void CallWaiter(string tableLogin);
	}
}