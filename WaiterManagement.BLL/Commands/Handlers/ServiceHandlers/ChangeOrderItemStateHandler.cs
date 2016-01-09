using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Models;

namespace WaiterManagement.BLL.Commands.Handlers.ServiceHandlers
{
	public class ChangeOrderItemStateHandler : Handler, IHandleCommand<ChangeOrderItemStateCommand>
	{
		public void Handle(ChangeOrderItemStateCommand command)
		{
			//TODO: Zapisać aktualny stan do bazy danych?

			var order = UnitOfWork.Get<Order>(command.OrderId);
			UnitOfWork.Load(order, o => o.Table);

			var menuItemQuantity = UnitOfWork.Get<MenuItemsQuantity>(command.MenuItemQuantityId);
			UnitOfWork.Load(menuItemQuantity, miQtd => miQtd.Item);

			menuItemQuantity.Ready = command.Ready;

			EventBus.PublishEvent(new OrderItemStateChanged()
			{
				OrderId = command.OrderId, TableLogin = order.Table.Title, OrderItemState = new OrderItemState()
				{
					MenuItemId = menuItemQuantity.Item.Id,
					Quantity = menuItemQuantity.Quantity,
					Ready = command.Ready
				}
			});
		}
	}
}
