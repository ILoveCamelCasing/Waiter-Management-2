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
		private Func<string> _callingTableConnectionId;
		private Func<string> _callingWaiterConnectionId;

		public void SetRetriveTableMethod(Func<IHubCallerConnectionContext<ITableApp>> tables)
		{
			_tables = tables;
		}

		public void SetRetriveWaiterMethod(Func<IHubCallerConnectionContext<IWaiterApp>> waiters)
		{
			_waiters = waiters;
		}

		public void SetCallingTableMethod(Func<string> callingTableConnectionId)
		{
			_callingTableConnectionId = callingTableConnectionId;
		}

		public void SetCallingWaiterMethod(Func<string> callingWaiterConnectionId)
		{
			_callingWaiterConnectionId = callingWaiterConnectionId;
		}

		public ITableApp GetTable(string login)
		{
			if (_tables == null)
				throw new InvalidOperationException("Method to getting table not set.");

			return _tables.Invoke().User(login);
		}

		public IWaiterApp GetWaiter(string login)
		{
			if (_waiters == null)
				throw new InvalidOperationException("Method to getting waiters not set.");

			return _waiters.Invoke().User(login);
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

		public IWaiterApp GetWaitersExcept(string login)
		{
			if(_waiters == null)
				throw new InvalidOperationException("Method to getting waiters not set.");

			if (_callingWaiterConnectionId != null)
			{
				var callingWaiterConnectionId = _callingWaiterConnectionId.Invoke();
				if (!String.IsNullOrWhiteSpace(callingWaiterConnectionId))
					return _waiters.Invoke().AllExcept(new[] { callingWaiterConnectionId });
			}

			return _waiters.Invoke().All;
		}
	}

	public interface ICallingServiceSubscriber
	{
		void SetRetriveTableMethod(Func<IHubCallerConnectionContext<ITableApp>> tables);
		void SetCallingTableMethod(Func<string> callingTableConnectionId);
		void SetRetriveWaiterMethod(Func<IHubCallerConnectionContext<IWaiterApp>> waiters);
		void SetCallingWaiterMethod(Func<string> callingWaiterConnectionId);
	}
}