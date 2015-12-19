using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.Common;
using WaiterManagement.Common.Apps;
using WaiterManagement.Service.Security;

namespace WaiterManagement.Service.Hubs
{
	[HubName("waiterHub")]
	[CustomAuthorize]
	public class WaiterHub : Hub<IWaiterApp>
	{
		private readonly ICommandBus _commandBus;

		public WaiterHub(ICommandBus commandBus, ICallingService callingService)
		{
			_commandBus = commandBus;
			callingService.SetRetriveWaiterMethod(() => Clients.All);
		}
	}
}