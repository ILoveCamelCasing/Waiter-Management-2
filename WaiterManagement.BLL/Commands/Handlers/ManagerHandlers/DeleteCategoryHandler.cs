using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Commands.Handlers.ManagerHandlers
{
	public class DeleteCategoryHandler : Handler, IHandleCommand<DeleteCategoryCommand>
	{
		public void Handle(DeleteCategoryCommand command)
		{
			var currentTable = UnitOfWork.Get<Category>(command.Id);
			var newVersion = (Category) currentTable.CreateDeletedVersion(UnitOfWork);

			UnitOfWork.Add(newVersion);
		}
	}
}