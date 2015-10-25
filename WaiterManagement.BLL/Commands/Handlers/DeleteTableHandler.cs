﻿using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Handlers
{
	public class DeleteTableHandler : Handler, IHandleCommand<DeleteTableCommand>
	{
		public DeleteTableHandler(IUnitOfWork unitUnitOfWork)
			: base(unitUnitOfWork)
		{
		}

		public void Handle(DeleteTableCommand command)
		{
			var currentTable = UnitOfWork.Get<Table>(command.Id);
			var newVersion = (Table) currentTable.CreateDeletedVersion(UnitOfWork);

			UnitOfWork.Add(newVersion);
		}
	}
}