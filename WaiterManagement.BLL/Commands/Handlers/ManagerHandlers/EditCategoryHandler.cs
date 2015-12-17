using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Commands.Handlers.ManagerHandlers
{
	public class EditCategoryHandler: Handler, IHandleCommand<EditCategoryCommand>
	{
		public void Handle(EditCategoryCommand command)
		{
			var currentcategory = UnitOfWork.Get<Category>(command.Id);
			var newVersion = (Category)currentcategory.CreateNewVersion(UnitOfWork);
			newVersion.Title = command.Title;
			newVersion.Description = command.Description;
		}
	}
}