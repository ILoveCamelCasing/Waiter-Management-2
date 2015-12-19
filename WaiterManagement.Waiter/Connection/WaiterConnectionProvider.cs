using System.Configuration;
using Microsoft.AspNet.SignalR.Client;
using WaiterManagement.Common.Apps;
using WaiterManagement.Common.Models;
using WaiterManagement.Common.Security;
using WaiterManagement.Waiter.Bootstrapper;

namespace WaiterManagement.Waiter.Connection
{
	public class WaiterConnectionProvider : IWaiterConnectionProvider
	{
		private HubConnection _hubConnection;
		private IHubProxy _hubProxy;

		private readonly IAccessProvider _accessProvider;
		private readonly IWaiterApp _waiterApp;

		public WaiterConnectionProvider(IAccessProvider accessProvider, IWaiterApp waiterApp)
		{
			_accessProvider = accessProvider;
			_waiterApp = waiterApp;
		}

		public void Connect()
		{
			_hubConnection = new HubConnection(ConfigurationManager.AppSettings["ServerPath"]);
			_hubConnection.Headers.Add("login", _accessProvider.Login);
			_hubConnection.Headers.Add("token", _accessProvider.Token);
			_hubProxy = _hubConnection.CreateHubProxy("waiterHub");
			_hubProxy.On<OrderModel>("NewOrder", order => _waiterApp.NewOrder(order));
			_hubConnection.Start().Wait();
		}
	}
}