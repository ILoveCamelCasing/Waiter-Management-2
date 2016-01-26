using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Commands.Handlers.ServiceHandlers
{
	public class EndOrderHandler : Handler, IHandleCommand<EndOrderCommand>
	{
		public void Handle(EndOrderCommand command)
		{
			var order = UnitOfWork.Get<Order>(command.OrderId);
			UnitOfWork.Load(order, o => o.Table);

			order.Status = command.OrderCancelled ? OrderStatus.Cancelled : OrderStatus.Completed;

			if (command.OrderCancelled)
				order.Comment = command.OrderCancelledReason;

			EventBus.PublishEvent(new EndedOrder()
			{
				OrderId = command.OrderId,
				OrderCancelled = command.OrderCancelled,
				OrderCancelledReason = command.OrderCancelledReason,
				TableLogin = order.Table.Title
			});
		}
	}
}
