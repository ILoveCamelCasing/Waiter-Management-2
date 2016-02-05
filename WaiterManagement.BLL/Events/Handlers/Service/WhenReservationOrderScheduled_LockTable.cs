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
	public class WhenReservationOrderScheduled_LockTable : IHandleEvent<ReservationOrderScheduled>
	{
		#region Dependencies
		private readonly ICallingService _callingService;
		private readonly IUnitOfWork _unitOfWork;
		#endregion

		#region Constructors
		public WhenReservationOrderScheduled_LockTable(ICallingService callingService, IUnitOfWork unitOfWork)
		{
			if(callingService == null)
				throw new ArgumentNullException(nameof(callingService));
			if (unitOfWork == null)
				throw new ArgumentNullException(nameof(unitOfWork));

			_callingService = callingService;
			_unitOfWork = unitOfWork;
		}
		#endregion


		#region IHandleEvent
		public void Handle(ReservationOrderScheduled @event)
		{
			var menuItems = _unitOfWork.GetWhere<MenuItemsQuantity>(miq => miq.Order.Id == @event.Order.Id);

			var table = _callingService.GetTable(@event.Order.Table.Title);

			if (table == null)
			{
				//TODO: Ustawić stan na Cancelled
				return;
			}

			table.LockTable(new ReservationOrderScheduledModel()
			{
				UnlockCode = @event.UnlockCode,
				MenuItems =  menuItems.Select(miq => new OrderingMenuItem()
				{
					MenuItemId = miq.Item.Id,
					Quantities = miq.Quantity
				})
			});
		}
		#endregion
	}
}
