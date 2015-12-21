namespace WaiterManagement.Common.Apps
{
	public interface ITableApp
	{
		void NotifyTable(string message);
		void SendOrderId(int id);
	}
}