using System;
using Microsoft.AspNet.SignalR.Hubs;
using WaiterManagement.Common;
using WaiterManagement.Common.Apps;

namespace WaiterManagement.Service.Hubs
{
	public class CallingService : ICallingService, ICallingServiceSubscriber
	{
		private Func<IHubCallerConnectionContext<ITableApp>> _tables;
		private Func<IHubCallerConnectionContext<IWaiterApp>> _waiters;

		public void SetRetriveTableMethod(Func<IHubCallerConnectionContext<ITableApp>> tables)
		{
			_tables = tables;
		}

		public void SetRetriveWaiterMethod(Func<IHubCallerConnectionContext<IWaiterApp>> waiters)
		{
			_waiters = waiters;
		}

		public ITableApp GetTable(string login)
		{
			return _tables.Invoke().User(login);
		}

		public ITableApp GetTables()
		{
			if(_tables == null)
				throw new InvalidOperationException("Method to getting table not set.");

			return _tables.Invoke().All;
		}

		public IWaiterApp GetWaiters()
		{
			if(_waiters == null)
				throw new InvalidOperationException("Method to getting waiters not set.");

			return _waiters.Invoke().All;
		}
	}

	public interface ICallingServiceSubscriber
	{
		void SetRetriveTableMethod(Func<IHubCallerConnectionContext<ITableApp>> tables);
		void SetRetriveWaiterMethod(Func<IHubCallerConnectionContext<IWaiterApp>> waiters);
	}
}