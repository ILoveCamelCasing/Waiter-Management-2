using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Handlers
{
	public class AddCategoryHandler : Handler, IHandleCommand<AddCategoryCommand>
	{
		public AddCategoryHandler(IUnitOfWork unitUnitOfWork)
			: base(unitUnitOfWork)
		{
		}

		public void Handle(AddCategoryCommand command)
		{
			UnitOfWork.Add(new Category{Title = command.Title, Description = command.Description});
		}
	}
}