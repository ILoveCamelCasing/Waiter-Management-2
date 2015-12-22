using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.Common;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Commands.Handlers.ServiceHandlers
{
	public class CallWaiterHandler : Handler, IHandleCommand<CallWaiterCommand>
	{
		private readonly ICallingService _callingService;

		public CallWaiterHandler(ICallingService callingService)
		{
			_callingService = callingService;
		}

		public void Handle(CallWaiterCommand command)
		{
			//var assignedWaiter = UnitOfWork.GetFirstOrDefault<Order>(x => x.Table.User.Login == command.TableLogin);
			//if(assignedWaiter != null)
			//	_callingService.GetWaiters(assignedWaiter.)
			//_callingService.GetWaiters().CallWaiter(command.TableLogin);
		}
	}
}