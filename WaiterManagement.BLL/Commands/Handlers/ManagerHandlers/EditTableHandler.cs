using System;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Handlers.ManagerHandlers
{
	public class EditTableHandler: Handler, IHandleCommand<EditTableCommand>
	{
		public EditTableHandler(IUnitOfWork unitUnitOfWork)
			: base(unitUnitOfWork)
		{
		}

		public void Handle(EditTableCommand command)
		{
			if (UnitOfWork.AnyActual<Table>(x => x.Title == command.Title && x.Id != command.Id))
				throw new InvalidOperationException("Table with the same name exists.");

			var currentTable = UnitOfWork.Get<Table>(command.Id);

			if(!currentTable.IsNewest)
				throw new InvalidOperationException("You cant edit not newest table.");

			if (currentTable.IsDeleted)
				throw new InvalidOperationException("You cant edit deleted table.");

			var newVersion = (Table)currentTable.CreateNewVersion(UnitOfWork);
			newVersion.Title = command.Title;
			newVersion.Description = command.Description;

			if (newVersion.User.Login != command.Title)
			{
				var user = (User)newVersion.User.CreateNewVersion(UnitOfWork);
				user.Login = command.Title;

				newVersion.User = user;
			}
			
		}
	}
}