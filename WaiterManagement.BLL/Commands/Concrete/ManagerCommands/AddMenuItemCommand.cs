using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete.ManagerCommands
{
	public class AddMenuItemCommand : ICommand
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public int CategoryId { get; set; }
	}
}