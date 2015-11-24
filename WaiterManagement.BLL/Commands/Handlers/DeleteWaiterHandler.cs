using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Handlers
{
  public class DeleteWaiterHandler : Handler, IHandleCommand<DeleteWaiterCommand>
  {
    #region Constructors
    public DeleteWaiterHandler(IUnitOfWork unitOfWork)
      : base(unitOfWork)
    {    }
    #endregion

    #region IHandleCommand
    public void Handle(DeleteWaiterCommand command)
    {
      var waiter = UnitOfWork.Get<Waiter>(command.Id);
      var waiterNewVersion = (Waiter)waiter.CreateDeletedVersion(UnitOfWork);

      UnitOfWork.Add(waiterNewVersion);
    }
    #endregion
  }
}
