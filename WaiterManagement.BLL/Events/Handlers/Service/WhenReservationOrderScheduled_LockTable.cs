using System;
using WaiterManagement.BLL.Events.Base;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common;

namespace WaiterManagement.BLL.Events.Handlers.Service
{
	public class WhenReservationOrderScheduled_LockTable : IHandleEvent<ReservationOrderScheduled>
	{
		#region Dependencies
		private readonly ICallingService _callingService;
		#endregion

		#region Constructors
		public WhenReservationOrderScheduled_LockTable(ICallingService callingService)
		{
			if(callingService == null)
				throw new ArgumentNullException(nameof(callingService));

			_callingService = callingService;
		}
		#endregion


		#region IHandleEvent
		public void Handle(ReservationOrderScheduled @event)
		{
			
		}
		#endregion
	}
}
