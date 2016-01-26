using System.Threading.Tasks;
using WaiterManagement.Common.Models;

namespace WaiterManagement.Waiter.Connection
{
	public interface IWaiterConnectionProvider
	{
		Task Connect();

		void AcceptOrder(int orderId);
		void EndOrder(int orderId, bool wasCancelled, string cancelledReason);
		void ChangeOrderItemState(int orderId, AcceptedOrderMenuItemQuantity changedOrderItem);
		void UpdateAfterLogin();
	}
}