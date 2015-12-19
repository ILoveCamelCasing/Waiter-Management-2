using System;
using WaiterManagement.Common.Apps;

namespace WaiterManagement.Common
{
	public interface ICallingService
	{
		void SetRetriveTableMethod(Func<ITableApp> action);
		void SetRetriveWaiterMethod(Func<IWaiterApp> action);
		ITableApp GetTables();
		IWaiterApp GetWaiters();
	}
}