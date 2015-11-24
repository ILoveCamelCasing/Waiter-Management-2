using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete.ManagerCommands
{
	public class DeleteMenuItemCommand : ICommand
	{
		public int Id { get; set; }
	}
}