using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.Common.Models;

namespace WaiterManagement.Service.Hubs
{
	[HubName("tableHub")]
	public class TableHub : Hub
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
				MenuItemsQuantities = order.OrderingMenuItems
			});
		}
	}
}