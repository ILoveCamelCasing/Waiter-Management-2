using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete.ServiceCommands
{
	public class AcceptOrderCommand : ICommand
	{
		public int OrderId { get; set; }
		public string WaiterLogin { get; set; }
	}
}
