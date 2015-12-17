using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Commands.Handlers.ManagerHandlers
{
	public class DeleteWaiterHandler : Handler, IHandleCommand<DeleteWaiterCommand>
	{
		public void Handle(DeleteWaiterCommand command)
		{
			var waiter = UnitOfWork.Get<Waiter>(command.Id);
			var waiterNewVersion = (Waiter)waiter.CreateDeletedVersion(UnitOfWork);

			UnitOfWork.Add(waiterNewVersion);
		}
	}
}
