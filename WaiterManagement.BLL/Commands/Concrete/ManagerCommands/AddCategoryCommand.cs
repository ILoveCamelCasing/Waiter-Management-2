﻿using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete.ManagerCommands
{
	public class AddCategoryCommand : ICommand
	{
		public string Title { get; set; }
		public string Description { get; set; }
	}
}