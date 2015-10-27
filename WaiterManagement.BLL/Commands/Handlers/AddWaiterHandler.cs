using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Handlers
{
  public class AddWaiterHandler : Handler, IHandleCommand<AddWaiterCommand>
  {
    #region Constructors
    public AddWaiterHandler(IUnitOfWork unitOfWork)
      :base(unitOfWork)
      { }
    #endregion

    #region IHandleCommand
    public void Handle(AddWaiterCommand command)
    {
      UnitOfWork.Add(new Waiter() { FirstName = command.FirstName, LastName = command.LastName });
    }
    #endregion
  }
}
