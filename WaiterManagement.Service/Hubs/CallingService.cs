using System;
using System.Collections.Generic;
using WaiterManagement.Common.Apps;

namespace WaiterManagement.Service.Hubs
{
	public class CallingService : ICallingService
	{
		private Func<IEnumerable<ITableApp>> _retriveTable;
		private Func<IEnumerable<IWaiterApp>> _retriveWaiters;

		public void SetRetriveTableMethod(Func<IEnumerable<ITableApp>> action)
		{
			_retriveTable = action;
		}

		public void SetRetriveWaiterMethod(Func<IEnumerable<IWaiterApp>> action)
		{
			_retriveWaiters = action;
		}

		public IEnumerable<ITableApp> GetTables()
		{
			if(_retriveTable == null)
				throw new InvalidOperationException("Method to getting table not set.");

			return _retriveTable.Invoke();
		}

		public IEnumerable<IWaiterApp> GetWaiters()
		{
			if(_retriveWaiters == null)
				throw new InvalidOperationException("Method to getting waiters not set.");

			return _retriveWaiters.Invoke();
		}
	}

	public interface ICallingService
	{
		void SetRetriveTableMethod(Func<IEnumerable<ITableApp>> action);
		void SetRetriveWaiterMethod(Func<IEnumerable<IWaiterApp>> action);
		IEnumerable<ITableApp> GetTables();
		IEnumerable<IWaiterApp> GetWaiters();
	}
}