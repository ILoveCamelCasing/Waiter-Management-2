﻿using System.Linq;
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
			var table = UnitOfWork.Get<Table>(x => x.User.Login == command.TableLogin);
			var order = new Order() {Created = SystemTime.Now, Status = OrderStatus.Created, Table = table};
			UnitOfWork.Add(order);
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
			EventBus.PublishEvent(new AddedOrder(){Order = order,TableTitle = command.TableLogin});
		}
	}
}