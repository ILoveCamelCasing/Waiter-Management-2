using System;
using System.Threading.Tasks;
using WaiterManagement.Common.Apps;
using WaiterManagement.Common.Models;

namespace WaiterManagement.Table.Bootstrapper
{
	public class TableApp : ITableApp, ITableAppSubscriber
	{
		#region ITableAppSubscriber
		public event EventHandler<string> NotifyEvent;
		public event EventHandler<int> SendOrderIdEvent;
		public event EventHandler<OrderItemState> OrderItemStateChangedEvent;
		#endregion

		#region ITableApp
		public void NotifyTable(string message)
		{
			Task.Run(() => HandleSafely(NotifyEvent, message)); //should be awaited...
		}

		public void NotifyOrderItemStateChanged(OrderItemState state)
		{
			Task.Run(() => HandleSafely(OrderItemStateChangedEvent, state)); //should be awaited...
		}

		public void SendOrderId(int id)
		{
			Task.Run(() => HandleSafely(SendOrderIdEvent, id)); //should be awaited...
		}
		#endregion

		#region Private Methods
		private void HandleSafely<T>(EventHandler<T> handler, T args)
		{
			if (handler != null)
			{
				try
				{
					handler(this, args);
				}
				catch
				{
					//Logging
				}
			}
		}
		#endregion
	}

	public interface ITableAppSubscriber
	{
		event EventHandler<string> NotifyEvent;
		event EventHandler<int> SendOrderIdEvent;
		event EventHandler<OrderItemState> OrderItemStateChangedEvent;
	}
}