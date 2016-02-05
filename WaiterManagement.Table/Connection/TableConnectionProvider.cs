using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using WaiterManagement.Common.Apps;
using WaiterManagement.Common.Models;
using WaiterManagement.Common.Security;
using WaiterManagement.Table.Model;

namespace WaiterManagement.Table.Connection
{
	public class TableConnectionProvider : ITableConnectionProvider
	{
		private HubConnection _hubConnection;
		private IHubProxy _hubProxy;

		private readonly IAccessProvider _accessProvider;
		private readonly ITableApp _tableApp;
		private readonly ICurrentOrder _currentOrder;

		public TableConnectionProvider(IAccessProvider accessProvider, ITableApp tableApp, ICurrentOrder currentOrder)
		{
			_accessProvider = accessProvider;
			_tableApp = tableApp;
			_currentOrder = currentOrder;
		}

		public async Task Connect()
		{
			if (_hubConnection != null && _hubConnection.State == ConnectionState.Connected)
				return;

			_hubConnection = new HubConnection(ConfigurationManager.AppSettings["ServerPath"]);
			_hubConnection.Headers.Add("login", _accessProvider.Login);
			_hubConnection.Headers.Add("token", _accessProvider.Token);
			_hubProxy = _hubConnection.CreateHubProxy("tableHub");
			_hubProxy.On<string>("NotifyTable", message => _tableApp.NotifyTable(message));
			_hubProxy.On<EndOrderModel>("NotifyOrderEnded", endedOrder => _tableApp.NotifyOrderEnded(endedOrder));
			_hubProxy.On<int>("SendOrderId", id => _tableApp.SendOrderId(id));
			_hubProxy.On<OrderItemState>("NotifyOrderItemStateChanged", state => _tableApp.NotifyOrderItemStateChanged(state));
			_hubProxy.On<ReservationOrderScheduledModel>("LockTable", scheduledOrder => _tableApp.LockTable(scheduledOrder));
			await _hubConnection.Start();
		}

		public void Disconnect()
		{
			if(_hubConnection != null)
				_hubConnection.Stop();
		}

		public void MakeNewOrder(IEnumerable<OrderMenuItemModel> orderingElements)
		{
			_hubProxy.Invoke("MakeNewOrder",
				new NewOrderModel()
				{
					TableLogin = _accessProvider.Login,
					OrderingMenuItems =
						orderingElements.Select(x => new OrderingMenuItem() { MenuItemId = x.Id, Quantities = x.Quantities })
				}).Wait();
		}

		public void OrderMoreItems(IEnumerable<OrderMenuItemModel> orderingElements)
		{
			_hubProxy.Invoke("OrderMoreItems",
				new MoreItemsModel()
				{
					OrderId = _currentOrder.CurrentOrderId,
					OrderingMenuItems =
						orderingElements.Select(x => new OrderingMenuItem() {MenuItemId = x.Id, Quantities = x.Quantities})
				}).Wait();
		}

		public void CallWaiter()
		{
			_hubProxy.Invoke("CallWaiter").Wait();
		}
	}
}