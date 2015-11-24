using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete.ManagerCommands
{
  public class DeleteWaiterCommand : ICommand
  {
    public int Id { get; set; }
  }
}
