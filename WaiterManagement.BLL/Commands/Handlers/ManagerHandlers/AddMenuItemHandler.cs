using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Handlers.ManagerHandlers
{
	public class AddMenuItemHandler : Handler, IHandleCommand<AddMenuItemCommand>
	{
		public AddMenuItemHandler(IUnitOfWork unitUnitOfWork)
			: base(unitUnitOfWork)
		{
		}

		public void Handle(AddMenuItemCommand command)
		{
			var category = UnitOfWork.Get<Category>(command.CategoryId);
			UnitOfWork.Add(new MenuItem{Title = command.Title, Description = command.Description, Category = category});
		}
	}
}