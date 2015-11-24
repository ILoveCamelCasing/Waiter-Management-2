using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Handlers
{
	public class DeleteCategoryHandler : Handler, IHandleCommand<DeleteCategoryCommand>
	{
		public DeleteCategoryHandler(IUnitOfWork unitUnitOfWork)
			: base(unitUnitOfWork)
		{
		}

		public void Handle(DeleteCategoryCommand command)
		{
			var currentTable = UnitOfWork.Get<Category>(command.Id);
			var newVersion = (Category) currentTable.CreateDeletedVersion(UnitOfWork);

			UnitOfWork.Add(newVersion);
		}
	}
}