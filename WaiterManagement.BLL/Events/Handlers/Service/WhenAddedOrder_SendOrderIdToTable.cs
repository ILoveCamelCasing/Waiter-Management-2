using WaiterManagement.BLL.Events.Base;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common;

namespace WaiterManagement.BLL.Events.Handlers.Service
{
	public class WhenAddedOrder_SendOrderIdToTable : IHandleEvent<AddedOrder>
	{
		private readonly ICallingService _callingService;

		public WhenAddedOrder_SendOrderIdToTable(ICallingService callingService)
		{
			_callingService = callingService;
		}

		public void Handle(AddedOrder @event)
		{
			_callingService.GetTable(@event.TableTitle).SendOrderId(@event.Order.Id);
		}
	}
}