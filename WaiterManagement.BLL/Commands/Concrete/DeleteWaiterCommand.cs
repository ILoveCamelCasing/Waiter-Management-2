using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete
{
  public class DeleteWaiterCommand : ICommand
  {
    public int Id { get; set; }
  }
}
