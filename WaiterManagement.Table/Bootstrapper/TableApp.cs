using System.Threading.Tasks;
using WaiterManagement.Common.Apps;

namespace WaiterManagement.Table.Bootstrapper
{
	public delegate void NotifyEventHandler(string message);

	public delegate void SendOrderIdHandler(int id);

	public class TableApp : ITableApp, ITableAppSubscriber
	{
		public event NotifyEventHandler NotifyEvent;
		public event SendOrderIdHandler SendOrderIdEvent;

		public void NotifyTable(string message)
		{
			var handler = NotifyEvent;
			if (handler != null)
				Task.Factory.StartNew(() =>  handler(message));
		}

		public void SendOrderId(int id)
		{
			var handler = SendOrderIdEvent;
			if (handler != null)
				Task.Factory.StartNew(() =>handler(id));
		}
	}

	public interface ITableAppSubscriber
	{
		event NotifyEventHandler NotifyEvent;
		event SendOrderIdHandler SendOrderIdEvent;

	}
}