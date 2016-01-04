using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.Common.Apps;
using WaiterManagement.Common.Models;
using WaiterManagement.Service.Security;

namespace WaiterManagement.Service.Hubs
{
	[HubName("waiterHub")]
	[CustomAuthorize]
	public class WaiterHub : Hub<IWaiterApp>
	{
		#region Dependencies
		private readonly ICommandBus _commandBus;
		#endregion

		#region Constructors
		public WaiterHub(ICommandBus commandBus, ICallingServiceSubscriber callingService)
		{
			_commandBus = commandBus;
			callingService.SetRetriveWaiterMethod(() => Clients);
			callingService.SetCallingWaiterMethod(() => Context.ConnectionId);
		}
		#endregion

		#region Hub Methods
		public void AcceptOrder(AcceptOrderModel acceptedOrder)
		{
			_commandBus.SendCommand(new AcceptOrderCommand()
			{
				OrderId = acceptedOrder.OrderId,
				WaiterLogin = GetCallerLogin()
			});
		}
		#endregion

		#region Private Methods
		private string GetCallerLogin() //Do wspólnej klasy bazowej WaiterHub/TableHub
		{
			var login = Context.Headers["login"];
			return login;
		}
		#endregion
	}
}