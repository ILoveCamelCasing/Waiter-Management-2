using System;
using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete.ServiceCommands
{
	public class NewWebOrderCommand : ICommand
	{
		public string Login { get; set; }
		public DateTime OrderDate { get; set; }
		public NewWebOrderCommandItem[] Items { get; set; }
	}

	public class NewWebOrderCommandItem
	{
		public int ItemId { get; set; }
		public int Quantity { get; set; }
	}
}