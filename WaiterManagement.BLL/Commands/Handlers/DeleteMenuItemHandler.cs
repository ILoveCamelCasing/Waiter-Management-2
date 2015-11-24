using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Handlers
{
	public class DeleteMenuItemHandler : Handler, IHandleCommand<DeleteMenuItemCommand>
	{
		public DeleteMenuItemHandler(IUnitOfWork unitUnitOfWork)
			: base(unitUnitOfWork)
		{
		}

		public void Handle(DeleteMenuItemCommand command)
		{
			var currentItem = UnitOfWork.Get<MenuItem>(command.Id);
			var newVersion = (MenuItem)currentItem.CreateDeletedVersion(UnitOfWork);

			UnitOfWork.Add(newVersion);
		}
	}
}