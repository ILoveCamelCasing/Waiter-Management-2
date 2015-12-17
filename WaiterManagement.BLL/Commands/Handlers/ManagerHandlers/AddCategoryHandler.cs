using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Commands.Handlers.ManagerHandlers
{
	public class AddCategoryHandler : Handler, IHandleCommand<AddCategoryCommand>
	{
		public void Handle(AddCategoryCommand command)
		{
			UnitOfWork.Add(new Category{Title = command.Title, Description = command.Description});
		}
	}
}