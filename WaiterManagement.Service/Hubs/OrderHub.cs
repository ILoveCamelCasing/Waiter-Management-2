using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace WaiterManagement.Service.Hubs
{
	[HubName("orderHub")]
	public class OrderHub : Hub
	{
		public void Hello()
		{
			Clients.All.hello();
		}
	}
}