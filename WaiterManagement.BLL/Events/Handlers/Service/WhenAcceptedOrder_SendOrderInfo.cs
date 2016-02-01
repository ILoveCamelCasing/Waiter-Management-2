﻿using System;
using System.Linq;
using WaiterManagement.BLL.Events.Base;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;
using WaiterManagement.Common.Models;

namespace WaiterManagement.BLL.Events.Handlers.Service
{
	public class WhenAcceptedOrder_SendOrderInfo : IHandleEvent<AcceptedOrder>
	{
		#region Dependencies
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICallingService _callingService;
		#endregion

		#region Constructors
		public WhenAcceptedOrder_SendOrderInfo(IUnitOfWork unitOfWork,  ICallingService callingService)
		{
			if (unitOfWork == null)
				throw new ArgumentNullException(nameof(unitOfWork));
			if (callingService == null)
				throw new ArgumentNullException(nameof(callingService));

			_unitOfWork = unitOfWork;
			_callingService = callingService;
		}
		#endregion

		#region IHandleEvent
		public void Handle(AcceptedOrder @event)
		{
			var menuItems = _unitOfWork.GetWhere<MenuItemsQuantity>(mi => mi.Order.Id == @event.OrderId);

			var waiter = _callingService.GetWaiter(@event.WaiterLogin);

			waiter.AcceptedOrderInfoUpdated(new AcceptedOrderCurrentStateModel()
			{
				OrderId = @event.OrderId,
				MenuItems = menuItems.Select(mi => new AcceptedOrderMenuItemQuantity()
				{
					Ready = mi.Ready,
					MenuItemQuantityId = mi.Id,
					Quantity = mi.Quantity,
					MenuItem = new AcceptedOrderMenuItem()
					{
						Title = mi.Item.Title,
						Description = mi.Item.Description,
						Price = mi.Item.Price
					}
				})
			});
		}
		#endregion
	}
}
