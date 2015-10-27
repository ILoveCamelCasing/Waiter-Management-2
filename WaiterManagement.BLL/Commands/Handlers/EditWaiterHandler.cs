using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Handlers
{
  public class EditWaiterHandler : Handler, IHandleCommand<EditWaiterCommand>
  {
    #region Constructors
    public EditWaiterHandler(IUnitOfWork unitOfWork)
      :base(unitOfWork)
    { }
    #endregion

    #region IHandleCommand
    public void Handle(EditWaiterCommand command)
    {
      var waiter = UnitOfWork.Get<Waiter>(command.Id);
      var waiterNewVersion = (Waiter)waiter.CreateNewVersion(UnitOfWork);
      waiterNewVersion.FirstName = command.FirstName;
      waiterNewVersion.LastName = command.LastName;

      UnitOfWork.Add(waiterNewVersion);
    }
    #endregion
  }
}
