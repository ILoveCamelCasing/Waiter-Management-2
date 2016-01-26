using System;
using WaiterManagement.BLL.Events.Base;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common;
using WaiterManagement.Common.Models;

namespace WaiterManagement.BLL.Events.Handlers.Service
{
	public class WhenEndedOrder_NotifyTable : IHandleEvent<EndedOrder>
	{
		#region Dependencies
		private readonly ICallingService _callingService;
		#endregion

		#region Constructors

		public WhenEndedOrder_NotifyTable(ICallingService callingService)
		{
			if(callingService == null)
				throw new ArgumentNullException(nameof(callingService));

			_callingService = callingService;
		}
		#endregion

		#region IHandleEvent
		public void Handle(EndedOrder @event)
		{
			var table = _callingService.GetTable(@event.TableLogin);

			table.NotifyTable($"Your order (# {@event.OrderId}) was closed.");
			
			table.NotifyOrderEnded(new EndOrderModel()
			{
				OrderId = @event.OrderId,
				OrderCancelled = @event.OrderCancelled,
				OrderCancelledReason = @event.OrderCancelledReason
			});
		}
		#endregion
	}
}
