using System.Threading.Tasks;
using WaiterManagement.Common.Apps;
using WaiterManagement.Common.Models;

namespace WaiterManagement.Waiter.Bootstrapper
{
	public delegate void NewOrderHandler(OrderModel order);

	public class WaiterApp : IWaiterApp , IWaiterAppSubscriber
	{
		public event NewOrderHandler NotifyNewOrder;

		public void NewOrder(OrderModel order)
		{
			var handler = NotifyNewOrder;
			if (handler != null)
				Task.Factory.StartNew(() => handler(order));
		}

		public void CallWaiter(string tableLogin)
		{
			throw new System.NotImplementedException();
		}
	}

	public interface IWaiterAppSubscriber
	{
		event NewOrderHandler NotifyNewOrder;
	}
}