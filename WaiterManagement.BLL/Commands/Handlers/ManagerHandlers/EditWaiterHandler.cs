using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Commands.Handlers.ManagerHandlers
{
	public class EditWaiterHandler : Handler, IHandleCommand<EditWaiterCommand>
	{
		public void Handle(EditWaiterCommand command)
		{
			var waiter = UnitOfWork.Get<Waiter>(command.Id);
			var waiterNewVersion = (Waiter)waiter.CreateNewVersion(UnitOfWork);
			waiterNewVersion.FirstName = command.FirstName;
			waiterNewVersion.LastName = command.LastName;
		}
	}
}
