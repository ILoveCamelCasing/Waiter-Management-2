using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.Common.Apps;
using WaiterManagement.Common.Models;
using WaiterManagement.Service.Security;

namespace WaiterManagement.Service.Hubs
{
	[HubName("tableHub")]
	[CustomAuthorize]
	public class TableHub : Hub<ITableApp>
	{
		#region Dependencies

		private readonly ICommandBus _commundBus;

		#endregion

		#region Constructor

		public TableHub(ICommandBus commundBus)
		{
			_commundBus = commundBus;
		}

		#endregion

		public void MakeNewOrder(NewOrderModel order)
		{
			_commundBus.SendCommand(new AddOrderCommand()
			{
				TableLogin = order.TableLogin,
				MenuItemsQuantities = order.OrderingMenuItems,
				Table = Clients.Caller
			});
		}
	}
}