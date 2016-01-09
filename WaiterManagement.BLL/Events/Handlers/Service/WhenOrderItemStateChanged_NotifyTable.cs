using System;
using WaiterManagement.BLL.Events.Base;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common;

namespace WaiterManagement.BLL.Events.Handlers.Service
{
	public class WhenOrderItemStateChanged_NotifyTable : IHandleEvent<OrderItemStateChanged>
	{
		#region Dependencies
		private readonly ICallingService _callingService;
		#endregion

		#region Constructors
		public WhenOrderItemStateChanged_NotifyTable(ICallingService callingService)
		{
			if (callingService == null)
				throw new ArgumentNullException(nameof(callingService));

			_callingService = callingService;
		}

		#endregion

		#region IHandleEvent
		public void Handle(OrderItemStateChanged @event)
		{
			var table = _callingService.GetTable(@event.TableLogin);
			table.NotifyOrderItemStateChanged(@event.OrderItemState);
		}
		#endregion
	}
}
