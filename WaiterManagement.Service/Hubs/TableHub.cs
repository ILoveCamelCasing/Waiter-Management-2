using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace WaiterManagement.Service.Hubs
{
	[HubName("tableHub")]
	public class TableHub : Hub
	{
	}
}