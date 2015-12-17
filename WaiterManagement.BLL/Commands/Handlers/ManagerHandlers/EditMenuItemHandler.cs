using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Commands.Handlers.ManagerHandlers
{
	public class EditMenuItemHandler: Handler, IHandleCommand<EditMenuItemCommand>
	{
		public void Handle(EditMenuItemCommand command)
		{
			var currentMenuItem = UnitOfWork.Get<MenuItem>(command.Id);
			var newVersion = (MenuItem)currentMenuItem.CreateNewVersion(UnitOfWork);
			newVersion.Title = command.Title;
			newVersion.Description = command.Description;
			newVersion.Category = UnitOfWork.Get<Category>(command.CategoryId);
		}
	}
}