using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Handlers
{
	public class EditTableHandler: Handler, IHandleCommand<EditTableCommand>
	{
		public EditTableHandler(IUnitOfWork unitUnitOfWork)
			: base(unitUnitOfWork)
		{
		}

		public void Handle(EditTableCommand command)
		{
			var currentTable = UnitOfWork.Get<Table>(command.Id);
			var newVersion = (Table)currentTable.CreateNewVersion(UnitOfWork);
			newVersion.Title = command.Title;
			newVersion.Description = command.Description;

			UnitOfWork.Add(newVersion);
		}
	}
}