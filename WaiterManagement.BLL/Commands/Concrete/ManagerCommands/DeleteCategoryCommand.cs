﻿using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete.ManagerCommands
{
	public class DeleteCategoryCommand : ICommand
	{
		public int Id { get; set; }
	}
}