using WaiterManagement.Common.Apps;

namespace WaiterManagement.Common
{
	public interface ICallingService
	{
		ITableApp GetTables();
		IWaiterApp GetWaiters();
		ITableApp GetTable(string login);
	}
}