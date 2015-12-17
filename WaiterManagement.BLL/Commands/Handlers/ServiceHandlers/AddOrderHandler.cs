using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Commands.Handlers.ServiceHandlers
{
	public class AddOrderHandler : Handler, IHandleCommand<AddOrderCommand>
	{
		public void Handle(AddOrderCommand command)
		{
			var order = new Order() {Created = SystemTime.Now, Status = OrderStatus.Created};
			UnitOfWork.Add(order);
			EventBus.PublishEvent(new AddedOrder(){Order = order, Table = command.Table});
		}
	}
}