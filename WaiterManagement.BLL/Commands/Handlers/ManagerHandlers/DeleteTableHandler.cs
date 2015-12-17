using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Commands.Handlers.ManagerHandlers
{
	public class DeleteTableHandler : Handler, IHandleCommand<DeleteTableCommand>
	{
		public void Handle(DeleteTableCommand command)
		{
			var currentTable = UnitOfWork.Get<Table>(command.Id);
			var newVersion = (Table) currentTable.CreateDeletedVersion(UnitOfWork);

			UnitOfWork.Add(newVersion);
		}
	}
}