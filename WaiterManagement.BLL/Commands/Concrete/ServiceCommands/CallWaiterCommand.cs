using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete.ServiceCommands
{
	public class CallWaiterCommand : ICommand
	{
		public string TableLogin { get; set; }
	}
}