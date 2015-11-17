using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;
using WaiterManagement.Common.Security;

namespace WaiterManagement.BLL.Commands.Handlers
{
	public class ChangePasswordHandler : Handler, IHandleCommand<ChangePasswordCommand>
	{
		public ChangePasswordHandler(IUnitOfWork unitUnitOfWork) : base(unitUnitOfWork)
		{
		}

		public void Handle(ChangePasswordCommand command)
		{
			var entity = ((VersionableEntity) UnitOfWork.Get(command.EntityType, command.EntityId));
			var entityNewVersion = (ILoginableEntity)entity.CreateNewVersion(UnitOfWork);

			var user = (User)entityNewVersion.User.CreateNewVersion(UnitOfWork);
			user.SecondHash = HashUtility.CreateSecondHash(command.Password, user.Login);

			entityNewVersion.User = user;

		}
	}
}