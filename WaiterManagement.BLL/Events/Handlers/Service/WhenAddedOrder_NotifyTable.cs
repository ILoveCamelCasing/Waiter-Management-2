using WaiterManagement.BLL.Events.Base;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common;

namespace WaiterManagement.BLL.Events.Handlers.Service
{
	public class WhenAddedOrder_NotifyTable : IHandleEvent<AddedOrder>
	{
		private readonly ICallingService _callingService;

		public WhenAddedOrder_NotifyTable(ICallingService callingService)
		{
			_callingService = callingService;
		}

		public void Handle(AddedOrder command)
		{
			var table = _callingService.GetTable(command.TableTitle);
			table.NotifyTable(string.Format("Your order is {0}.", command.Order.Status));
		}
	}
}