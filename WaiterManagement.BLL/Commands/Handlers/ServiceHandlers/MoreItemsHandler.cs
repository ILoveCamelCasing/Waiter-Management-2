using System.Linq;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Commands.Handlers.ServiceHandlers
{
	public class MoreItemsHandler : Handler, IHandleCommand<MoreItemsCommand>
	{
		public void Handle(MoreItemsCommand command)
		{
			var order = UnitOfWork.Get<Order>(command.OrderId);
			var menuItemsIds = command.MenuItemsQuantities.Select(x => x.MenuItemId).ToArray();
			var menuItems = UnitOfWork.GetWhere<MenuItem>(x => menuItemsIds.Contains(x.Id)).ToArray();
			foreach (var menuItemsQuantityModel in command.MenuItemsQuantities)
			{
				var menuItemsQuantities = new MenuItemsQuantity()
				{
					Order = order,
					Quantity = menuItemsQuantityModel.Quantities,
					Item = menuItems.First(x => x.Id == menuItemsQuantityModel.MenuItemId)
				};
				UnitOfWork.Add(menuItemsQuantities);
			}
			EventBus.PublishEvent(new AddedMoreItems() { OrderId = command.OrderId });

		}
	}
}