using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete.ServiceCommands
{
	public class EndOrderCommand : ICommand
	{
		public int OrderId { get; set; }
		public bool OrderCancelled { get; set; }
		public string OrderCancelledReason { get; set; }
	}
}
