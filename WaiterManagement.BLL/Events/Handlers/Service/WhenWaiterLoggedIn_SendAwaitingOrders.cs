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
	public class WhenWaiterLoggedIn_SendAwaitingOrders : IHandleEvent<WaiterLoggedIn>
	{
		#region Private Fields
		private readonly ICallingService _callingService;
		private readonly IUnitOfWork _unitOfWork;

		public WhenWaiterLoggedIn_SendAwaitingOrders(ICallingService callingService, IUnitOfWork unitOfWork)
		{
			if (callingService == null)
				throw new ArgumentNullException(nameof(callingService));
			if (unitOfWork == null)
				throw new ArgumentNullException(nameof(unitOfWork));

			_callingService = callingService;
			_unitOfWork = unitOfWork;
		}

		#endregion

		public void Handle(WaiterLoggedIn @event)
		{
			var orders = _unitOfWork.GetWhere<Order>(o => o.Status == OrderStatus.Created);

			if (orders != null && orders.Any())
			{
				var waiter = _callingService.GetWaiter(@event.WaiterLogin);
				var orderModels = orders.Select(o => new OrderModel() {OrderId = o.Id, TableTitle = o.Table.Title});

				waiter.OrdersAwaiting(orderModels);
			}
		}
	}
}
