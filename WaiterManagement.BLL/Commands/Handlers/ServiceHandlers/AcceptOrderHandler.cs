﻿using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Commands.Handlers.ServiceHandlers
{
	public class AcceptOrderHandler : Handler, IHandleCommand<AcceptOrderCommand>
	{
		public void Handle(AcceptOrderCommand command)
		{
			var order = UnitOfWork.Get<Order>(command.OrderId);
			UnitOfWork.Load(order, o => o.Table);

			var waiter = UnitOfWork.GetActual<Waiter>(w => w.User.Login == command.WaiterLogin);

			order.Waiter = waiter;
			order.Status = OrderStatus.Assigned;

			EventBus.PublishEvent(new AcceptedOrder()
			{
				OrderId = command.OrderId,
				WaiterLogin = command.WaiterLogin,
				TableLogin = order.Table.Title
			});
		}
	}
}
