using System;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;
using WaiterManagement.Common.Security;

namespace WaiterManagement.BLL.Commands.Handlers
{
	public class AddTableHandler : Handler, IHandleCommand<AddTableCommand>
	{
		public AddTableHandler(IUnitOfWork unitUnitOfWork) : base(unitUnitOfWork)
		{
		}

		public void Handle(AddTableCommand command)
		{
			if(UnitOfWork.AnyActual<Table>(x => x.Title == command.Title))
				throw new InvalidOperationException("Table with the same name exists.");

			var login = string.Format("Table{0}", command.Title);
			var secondHash = HashUtility.CreateSecondHash(command.Password, login);

			var addedUser = UnitOfWork.Add(new User() {SecondHash = secondHash, Login = login});

			UnitOfWork.Add(new Table{Title = command.Title, Description = command.Description, User = addedUser});
		}
	}
}