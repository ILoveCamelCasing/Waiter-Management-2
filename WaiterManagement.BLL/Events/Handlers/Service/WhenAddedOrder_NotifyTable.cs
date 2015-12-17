using WaiterManagement.BLL.Events.Base;
using WaiterManagement.BLL.Events.Concrete.Service;

namespace WaiterManagement.BLL.Events.Handlers.Service
{
	public class WhenAddedOrder_NotifyTable : IHandleEvent<AddedOrder>
	{
		public void Handle(AddedOrder command)
		{
			command.Table.NotifyTable(string.Format("Your order is {0}.", command.Order.Status));
		}
	}
}