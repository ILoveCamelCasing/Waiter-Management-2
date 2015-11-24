using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;

namespace WaiterManagement.Service.Hubs
{
	[HubName("orderHub")]
	public class OrderHub : Hub
	{
		#region Dependencies

		private readonly ICommandBus _commundBus;

		#endregion

		#region Constructor

		public OrderHub(ICommandBus commundBus)
		{
			_commundBus = commundBus;
		}

		#endregion

		#region Public Methods

		public void AddOrder(Guid token, OrderModel order)
		{
			_commundBus.SendCommand(new AddOrderCommand(){});
		}

		#endregion
	}

	public class OrderModel
	{
	}
}