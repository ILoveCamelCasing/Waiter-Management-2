using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Commands.Handlers.ManagerHandlers
{
	public class DeleteMenuItemHandler : Handler, IHandleCommand<DeleteMenuItemCommand>
	{
		public void Handle(DeleteMenuItemCommand command)
		{
			var currentItem = UnitOfWork.Get<MenuItem>(command.Id);
			var newVersion = (MenuItem)currentItem.CreateDeletedVersion(UnitOfWork);

			UnitOfWork.Add(newVersion);
		}
	}
}