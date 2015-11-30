using System.Collections.Generic;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.Common.Models;

namespace WaiterManagement.BLL.Commands.Concrete.ServiceCommands
{
	public class AddOrderCommand : ICommand
	{
		public string TableLogin { get; set; }
		public IEnumerable<OrderingMenuItem> MenuItemsQuantities { get; set; }
	}
}