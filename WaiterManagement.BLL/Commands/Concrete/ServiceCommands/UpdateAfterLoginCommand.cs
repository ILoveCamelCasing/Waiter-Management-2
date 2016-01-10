using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete.ServiceCommands
{
	public class UpdateAfterWaiterLoginCommand : ICommand
	{
		public string WaiterLogin { get; set; }
	}
}
