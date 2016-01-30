using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete.ServiceCommands
{
	public class AddWebClientCommand : ICommand
	{
		public string Login { get; set; }
		public string FirstHash { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Mail { get; set; }
	}
}