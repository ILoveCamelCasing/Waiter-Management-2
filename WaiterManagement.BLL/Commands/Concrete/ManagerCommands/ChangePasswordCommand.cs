using System;
using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete.ManagerCommands
{
	public class ChangePasswordCommand : ICommand
	{
		public int EntityId { get; set; }
		public Type EntityType { get; set; }
		public string Password { get; set; }
	}
}