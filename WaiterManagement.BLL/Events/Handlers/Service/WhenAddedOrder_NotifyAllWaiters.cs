using WaiterManagement.BLL.Events.Base;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common;
using WaiterManagement.Common.Models;

namespace WaiterManagement.BLL.Events.Handlers.Service
{
	public class WhenAddedOrder_NotifyAllWaiters : IHandleEvent<AddedOrder>
	{
		private readonly ICallingService _callingService;

		public WhenAddedOrder_NotifyAllWaiters(ICallingService callingService)
		{
			_callingService = callingService;
		}

		public void Handle(AddedOrder command)
		{
			_callingService.GetWaiters().NewOrderMade(new OrderModel(){OrderId = command.Order.Id, TableTitle = command.TableTitle});
		}
	}
}