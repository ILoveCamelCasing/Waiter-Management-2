using System;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Security;

namespace WaiterManagement.BLL.Commands.Handlers.ManagerHandlers
{
	public class AddWaiterHandler : Handler, IHandleCommand<AddWaiterCommand>
	{
		private readonly IPasswordManager _passwordManager;
		public AddWaiterHandler(IPasswordManager passwordManager)
		{
			_passwordManager = passwordManager;
		}

		public void Handle(AddWaiterCommand command)
		{
			if (UnitOfWork.AnyActual<Waiter>(x => x.User.Login == command.Login))
				throw new InvalidOperationException("Waiter with the same login exists.");

			var secondHash = _passwordManager.CreateSecondHash(command.Login, command.Password);

			var addedUser = UnitOfWork.Add(new User() { SecondHash = secondHash, Login = command.Login });

			UnitOfWork.Add(new Waiter() { FirstName = command.FirstName, LastName = command.LastName, User = addedUser });
		}
	}
}
