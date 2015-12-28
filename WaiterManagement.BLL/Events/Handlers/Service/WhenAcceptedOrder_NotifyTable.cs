using System;
using WaiterManagement.BLL.Events.Base;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common;

namespace WaiterManagement.BLL.Events.Handlers.Service
{
	public class WhenAcceptedOrder_NotifyTable : IHandleEvent<AcceptedOrder>
	{
		#region Dependencies
		private readonly ICallingService _callingService;
		#endregion

		#region Constructors
		public WhenAcceptedOrder_NotifyTable(ICallingService callingService)
		{
			if (callingService == null)
				throw new ArgumentNullException("callingService");

			_callingService = callingService;
		}
		#endregion

		#region IHandleEvent
		public void Handle(AcceptedOrder @event)
		{
			var table = _callingService.GetTable(@event.TableLogin);
			table.NotifyTable($"Your order was accepted by {@event.TableLogin}.");
		}
		#endregion
	}
}
