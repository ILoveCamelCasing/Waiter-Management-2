using System;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Security;

namespace WaiterManagement.BLL.Commands.Handlers.ManagerHandlers
{
	public class AddTableHandler : Handler, IHandleCommand<AddTableCommand>
	{
		private readonly IPasswordManager _passwordManager;

		public AddTableHandler(IPasswordManager passwordManager)
		{
			_passwordManager = passwordManager;
		}

		public void Handle(AddTableCommand command)
		{
			if(UnitOfWork.AnyActual<Table>(x => x.Title == command.Title))
				throw new InvalidOperationException("Table with the same name exists.");

			var login = command.Title;
			var secondHash = _passwordManager.CreateSecondHash(login, command.Password);

			var addedUser = UnitOfWork.Add(new User() {SecondHash = secondHash, Login = login});

			UnitOfWork.Add(new Table{Title = command.Title, Description = command.Description, User = addedUser});
		}
	}
}