using System;
using WaiterManagement.BLL.Events.Base;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common;
using WaiterManagement.Common.Models;

namespace WaiterManagement.BLL.Events.Handlers.Service
{
	public class WhenAcceptedOrder_SendInfoToOtherWaiters : IHandleEvent<AcceptedOrder>
	{
		#region Dependencies
		private readonly ICallingService _callingService;
		#endregion

		#region Constructors
		public WhenAcceptedOrder_SendInfoToOtherWaiters(ICallingService callingService)
		{
			if (callingService == null)
				throw new ArgumentNullException("callingService");

			_callingService = callingService;
		}
		#endregion

		#region IHandleEvent
		public void Handle(AcceptedOrder @event)
		{
			var waiters = _callingService.GetWaitersExcept(@event.WaiterLogin);
			waiters.OrderWasAccepted(new AcceptOrderModel()
			{
				OrderId = @event.OrderId
			});
		}
		#endregion
	}
}
