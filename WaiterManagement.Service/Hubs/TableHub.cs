using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.Common;
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

		public TableHub(ICommandBus commundBus, ICallingServiceSubscriber callingService)
		{
			_commundBus = commundBus;

			callingService.SetRetriveTableMethod(() => Clients);
		}

		#endregion

		#region Hub methods

		public void MakeNewOrder(NewOrderModel order)
		{
			_commundBus.SendCommand(new AddOrderCommand()
			{
				
				TableLogin = order.TableLogin,
				MenuItemsQuantities = order.OrderingMenuItems,
			});
		}

		public void OrderMoreItems(MoreItemsModel model)
		{
			_commundBus.SendCommand(new MoreItemsCommand()
			{
				OrderId = model.OrderId,
				MenuItemsQuantities = model.OrderingMenuItems
			});
		}

		public void CallWaiter()
		{
			_commundBus.SendCommand(new CallWaiterCommand()
			{
				TableLogin = GetCallerLogin()
			});
		}

		#endregion

		#region Private methods

		private string GetCallerLogin()
		{
			var login = Context.Headers["login"];
			return login;
		}

		#endregion
	}
}