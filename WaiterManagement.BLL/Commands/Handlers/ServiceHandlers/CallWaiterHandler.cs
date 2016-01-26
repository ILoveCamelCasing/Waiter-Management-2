using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.Common;
using WaiterManagement.Common.Apps;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Handlers.ServiceHandlers
{
	public class CallWaiterHandler : Handler, IHandleCommand<CallWaiterCommand>
	{
		private readonly ICallingService _callingService;
		private readonly IUnitOfWork _unitOfWork;

		public CallWaiterHandler(ICallingService callingService, IUnitOfWork unitOfWork)
		{
			_callingService = callingService;
			_unitOfWork = unitOfWork;
		}

		public void Handle(CallWaiterCommand command)
		{
			var order = _unitOfWork.GetFirstOrDefault<Order>(o => o.Status == OrderStatus.Assigned && o.Table.Title == command.TableLogin);

			IWaiterApp waiter;

			if (order != null)
			{
				_unitOfWork.Load(order, o => o.Waiter);
				_unitOfWork.Load(order.Waiter, w => w.User);
				waiter = _callingService.GetWaiter(order.Waiter.User.Login);
			}
			else
				waiter = _callingService.GetWaiters();

			waiter.CallWaiter(command.TableLogin);
		}
	}
}