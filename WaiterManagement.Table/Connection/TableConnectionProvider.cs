using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.AspNet.SignalR.Client;
using WaiterManagement.Common.Apps;
using WaiterManagement.Common.Models;
using WaiterManagement.Table.Model;

namespace WaiterManagement.Table.Connection
{
	public class TableConnectionProvider : ITableConnectionProvider
	{
		private HubConnection _hubConnection;
		private IHubProxy _hubProxy;

		private readonly IAccessProvider _accessProvider;
		private readonly ITableApp _tableApp;

		public TableConnectionProvider(IAccessProvider accessProvider, ITableApp tableApp)
		{
			_accessProvider = accessProvider;
			_tableApp = tableApp;
		}

		public void MakeNewOrder(IEnumerable<OrderMenuItemModel> orderingElements)
		{
			if(_hubConnection == null || _hubConnection.State != ConnectionState.Connected)
				Connect();

			_hubProxy.Invoke("MakeNewOrder",
				new NewOrderModel()
				{
					TableLogin = _accessProvider.TableLogin,
					OrderingMenuItems =
						orderingElements.Select(x => new OrderingMenuItem() { MenuItemId = x.Id, Quantities = x.Quantities })
				}).Wait();
		}

		private void Connect()
		{
			_hubConnection = new HubConnection(ConfigurationManager.AppSettings["ServerPath"]);
			_hubConnection.Headers.Add("login", _accessProvider.TableLogin);
			_hubConnection.Headers.Add("token", _accessProvider.Token);
			_hubProxy = _hubConnection.CreateHubProxy("tableHub");
			_hubProxy.On<string>("NotifyTable", message => _tableApp.NotifyTable(message));
			_hubConnection.Start().Wait();
		}
	}
}