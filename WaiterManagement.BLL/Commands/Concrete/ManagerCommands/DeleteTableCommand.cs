﻿using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete.ManagerCommands
{
	public class DeleteTableCommand : ICommand
	{
		public int Id { get; set; }
	}
}