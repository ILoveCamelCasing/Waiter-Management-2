using System;
using System.Linq;
using WaiterManagement.BLL.Events.Base;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;
using WaiterManagement.Common.Models;

namespace WaiterManagement.BLL.Events.Handlers.Service
{
	public class WhenAddedMoreItems_SendUpdatedOrderInfo : IHandleEvent<AddedMoreItems>
	{
		#region Dependencies
		private readonly ICallingService _callingService;
		private readonly IUnitOfWork _unitOfWork;
		#endregion

		#region Constructors
		public WhenAddedMoreItems_SendUpdatedOrderInfo(ICallingService callingService, IUnitOfWork unitOfWork)
		{
			if (callingService == null)
				throw new ArgumentNullException("callingService");
			if (unitOfWork == null)
				throw new ArgumentNullException("unitOfWork");

			_callingService = callingService;
			_unitOfWork = unitOfWork;
		}
		#endregion

		#region IHandleEvent
		public void Handle(AddedMoreItems @event)
		{
			var menuItems = _unitOfWork.GetWhere<MenuItemsQuantity>(mi => mi.Order.Id == @event.OrderId);
			var order = _unitOfWork.Get<Order>(@event.OrderId);
			_unitOfWork.Load(order, o => o.Waiter);
			_unitOfWork.Load(order.Waiter, w => w.User);

			var waiter = _callingService.GetWaiter(order.Waiter.User.Login);

			waiter.AcceptedOrderInfoUpdated(new AcceptedOrderCurrentStateModel()
			{
				OrderId = @event.OrderId,
				MenuItems = menuItems.Select(mi => new AcceptedOrderMenuItemQuantity()
				{
					Quantity = mi.Quantity,
					MenuItem = new AcceptedOrderMenuItem()
					{
						Title = mi.Item.Title,
						Description = mi.Item.Description
					}
				})
			});
		}
		#endregion
	}
}
