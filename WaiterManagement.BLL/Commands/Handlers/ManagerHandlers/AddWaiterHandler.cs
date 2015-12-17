using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Commands.Handlers.ManagerHandlers
{
	public class AddWaiterHandler : Handler, IHandleCommand<AddWaiterCommand>
	{
		public void Handle(AddWaiterCommand command)
		{
			UnitOfWork.Add(new Waiter() { FirstName = command.FirstName, LastName = command.LastName });
		}
	}
}
