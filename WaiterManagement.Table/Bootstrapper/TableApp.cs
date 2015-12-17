using System.Threading.Tasks;
using WaiterManagement.Common.Apps;

namespace WaiterManagement.Table.Bootstrapper
{
	public delegate void NotifyEventHandler(string message);

	public class TableApp : ITableApp, ITableAppSubscriber
	{
		public event NotifyEventHandler Notify;
		public void NotifyTable(string message)
		{
			var handler = Notify;
			if (handler != null)
				Task.Factory.StartNew(() =>  handler(message));
		}
	}

	public interface ITableAppSubscriber
	{
		event NotifyEventHandler Notify;
	}
}