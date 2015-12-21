using System.Collections.Generic;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.Common.Models;

namespace WaiterManagement.BLL.Commands.Concrete.ServiceCommands
{
	public class MoreItemsCommand : ICommand
	{
		public int OrderId { get; set; }
		public IEnumerable<OrderingMenuItem> MenuItemsQuantities { get; set; }
	}
}