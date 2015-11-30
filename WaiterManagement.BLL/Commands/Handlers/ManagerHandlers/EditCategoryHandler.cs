using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Handlers.ManagerHandlers
{
	public class EditCategoryHandler: Handler, IHandleCommand<EditCategoryCommand>
	{
		public EditCategoryHandler(IUnitOfWork unitUnitOfWork)
			: base(unitUnitOfWork)
		{
		}

		public void Handle(EditCategoryCommand command)
		{
			var currentcategory = UnitOfWork.Get<Category>(command.Id);
			var newVersion = (Category)currentcategory.CreateNewVersion(UnitOfWork);
			newVersion.Title = command.Title;
			newVersion.Description = command.Description;
		}
	}
}