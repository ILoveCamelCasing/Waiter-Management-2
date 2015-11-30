using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.AspNet.SignalR.Client;
using WaiterManagement.Common.Models;
using WaiterManagement.Table.Model;

namespace WaiterManagement.Table.Connection
{
	public class TableConnectionProvider : ITableConnectionProvider
	{
		private readonly IAccessProvider _accessProvider;

		public TableConnectionProvider(IAccessProvider accessProvider)
		{
			_accessProvider = accessProvider;
		}

		public void MakeNewOrder(IEnumerable<OrderMenuItemModel> orderingElements)
		{
			var hubConnection = new HubConnection(ConfigurationManager.AppSettings["ServerPath"]);
			var hubProxy = hubConnection.CreateHubProxy("tableHub");
			hubConnection.Start().Wait();

			hubProxy.Invoke("MakeNewOrder",
				new NewOrderModel()
				{
					TableLogin = _accessProvider.TableLogin,
					OrderingMenuItems =
						orderingElements.Select(x => new OrderingMenuItem() {MenuItemId = x.Id, Quantities = x.Quantities})
				});
		}
	}
}