using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Commands.Handlers.ServiceHandlers
{
	public class NewWebOrderHandler : Handler, IHandleCommand<NewWebOrderCommand>
	{
		public void Handle(NewWebOrderCommand command)
		{
			var client = UnitOfWork.Get<WebClient>(x => x.User.Login == command.Login);
			var table = GetFreeTable(command.OrderDate);
			var order = new ReservationOrder()
			{
				Client = client,
				Created = DateTime.Now,
				Status = ReservationOrderStatus.Created,
				ReservationTime = command.OrderDate,
				UnlockCode = CreateUnlockCode(),
				Table = table
			};
			UnitOfWork.Add(order);
			var menuItemsIds = command.Items.Select(x => x.ItemId).ToArray();
			var menuItems = UnitOfWork.GetWhere<MenuItem>(x => menuItemsIds.Contains(x.Id)).ToArray();
			var addedMenuItemQuantities = new List<ReservationMenuItemQuantity>();
			foreach (var item in command.Items)
			{
				var menuItemsQuantities = new ReservationMenuItemQuantity()
				{
					ReservationOrder = order,
					Quantity = item.Quantity,
					Item = menuItems.First(x => x.Id == item.ItemId)
				};
				UnitOfWork.Add(menuItemsQuantities);
				addedMenuItemQuantities.Add(menuItemsQuantities);
			}
			EventBus.PublishEvent(new AddedWebOrder() { Order = order, MenuItems = addedMenuItemQuantities });
		}

		private Table GetFreeTable(DateTime orderDate)
		{
			var allReservations = UnitOfWork.All<ReservationOrder>();
			var table =
				UnitOfWork.GetActual<Table>(
					x =>
						x.IsNewest && !x.IsDeleted &&
						!allReservations
							.Any(
								r =>
									r.Status != ReservationOrderStatus.Cancelled && r.Table == x && r.ReservationTime.Year == orderDate.Year &&
									r.ReservationTime.Month == orderDate.Month && r.ReservationTime.Day == orderDate.Day));
			return table;
		}

		private string CreateUnlockCode()
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			var random = new Random();
			return new string(Enumerable.Repeat(chars, 4)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}