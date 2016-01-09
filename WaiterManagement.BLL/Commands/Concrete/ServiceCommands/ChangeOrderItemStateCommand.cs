using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete.ServiceCommands
{
	public class ChangeOrderItemStateCommand : ICommand
	{
		public int OrderId { get; set; }
		public int MenuItemQuantityId { get; set; }
		public bool Ready { get; set; }
	}
}
