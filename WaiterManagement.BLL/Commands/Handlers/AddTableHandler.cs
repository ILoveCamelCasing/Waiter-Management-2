﻿using System;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Handlers
{
	public class AddTableHandler : Handler, IHandleCommand<AddTableCommand>
	{
		public AddTableHandler(IUnitOfWork unitUnitOfWork) : base(unitUnitOfWork)
		{
		}

		public void Handle(AddTableCommand command)
		{
			if(UnitOfWork.AnyActual<Table>(x => x.Title == command.Title))
				throw new InvalidOperationException("Table with the same name exists.");

			UnitOfWork.Add(new Table{Title = command.Title, Description = command.Description});
		}
	}
}