using System;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Security;

namespace WaiterManagement.BLL.Commands.Handlers.ServiceHandlers
{
	public class AddWebClientHandler : Handler, IHandleCommand<AddWebClientCommand>
	{
		private readonly IPasswordManager _passwordManager;

		public AddWebClientHandler(IPasswordManager passwordManager)
		{
			_passwordManager = passwordManager;
		}

		public void Handle(AddWebClientCommand command)
		{
			if (UnitOfWork.AnyActual<WebClient>(x => x.User.Login == command.Login))
				throw new InvalidOperationException("Table with the same name exists.");

			var login = command.Login;
			var secondHash = _passwordManager.CreateSecondHashFromFirst(command.FirstHash);

			var addedUser = UnitOfWork.Add(new User() { SecondHash = secondHash, Login = login });

			UnitOfWork.Add(new WebClient { FirstName = command.FirstHash, LastName = command.LastName, Phone = command.Phone, Mail = command.Mail, User = addedUser });
		}
	}
}