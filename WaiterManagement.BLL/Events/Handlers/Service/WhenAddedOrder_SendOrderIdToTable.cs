using WaiterManagement.BLL.Events.Base;
using WaiterManagement.BLL.Events.Concrete.Service;

namespace WaiterManagement.BLL.Events.Handlers.Service
{
	public class WhenAddedOrder_SendOrderIdToTable : IHandleEvent<AddedOrder>
	{
		public void Handle(AddedOrder command)
		{
			command.Table.SendOrderId(command.Order.Id);
		}
	}
}