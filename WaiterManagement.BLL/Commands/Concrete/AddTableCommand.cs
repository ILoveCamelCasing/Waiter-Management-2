﻿using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete
{
	public class AddTableCommand : ICommand
	{
		public string Title { get; set; }
		public string Description { get; set; }
	}
}