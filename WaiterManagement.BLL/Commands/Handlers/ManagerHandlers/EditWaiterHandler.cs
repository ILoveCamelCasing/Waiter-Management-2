using System;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Commands.Handlers.ManagerHandlers
{
	public class EditWaiterHandler : Handler, IHandleCommand<EditWaiterCommand>
	{
		public void Handle(EditWaiterCommand command)
		{
			if (UnitOfWork.AnyActual<Waiter>(x => x.User.Login == command.Login && x.Id != command.Id))
				throw new InvalidOperationException("Waiter with the same login exists.");

			var currentTable = UnitOfWork.Get<Waiter>(command.Id);

			if (!currentTable.IsNewest)
				throw new InvalidOperationException("You cant edit not newest waiter.");

			if (currentTable.IsDeleted)
				throw new InvalidOperationException("You cant edit deleted waiter.");

			var newVersion = (Waiter)currentTable.CreateNewVersion(UnitOfWork);
			newVersion.FirstName = command.FirstName;
			newVersion.LastName = command.LastName;

			if (newVersion.User.Login != command.Login)
			{
				var user = (User)newVersion.User.CreateNewVersion(UnitOfWork);
				user.Login = command.Login;

				newVersion.User = user;
			}
		}
	}
}
