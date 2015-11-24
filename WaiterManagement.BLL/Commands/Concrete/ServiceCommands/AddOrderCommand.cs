using System.Collections.Generic;
using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete.ServiceCommands
{
	public class AddOrderCommand : ICommand
	{
		public int TableId { get; set; }

		// Key: Id, Value: Quantity
		public Dictionary<int,int> MenuItemsQuantities { get; set; }
	}
}