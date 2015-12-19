using System;
using WaiterManagement.Common;
using WaiterManagement.Common.Apps;

namespace WaiterManagement.Service.Hubs
{
	public class CallingService : ICallingService
	{
		private Func<ITableApp> _retriveTable;
		private Func<IWaiterApp> _retriveWaiters;

		public void SetRetriveTableMethod(Func<ITableApp> action)
		{
			_retriveTable = action;
		}

		public void SetRetriveWaiterMethod(Func<IWaiterApp> action)
		{
			_retriveWaiters = action;
		}

		public ITableApp GetTables()
		{
			if(_retriveTable == null)
				throw new InvalidOperationException("Method to getting table not set.");

			return _retriveTable.Invoke();
		}

		public IWaiterApp GetWaiters()
		{
			if(_retriveWaiters == null)
				throw new InvalidOperationException("Method to getting waiters not set.");

			return _retriveWaiters.Invoke();
		}
	}
}