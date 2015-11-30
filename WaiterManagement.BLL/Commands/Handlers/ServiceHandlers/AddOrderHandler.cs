using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.Common;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Handlers.ServiceHandlers
{
	public class AddOrderHandler : Handler, IHandleCommand<AddOrderCommand>
	{
		public AddOrderHandler(IUnitOfWork unitUnitOfWork) : base(unitUnitOfWork)
		{
		}

		public void Handle(AddOrderCommand command)
		{
			UnitOfWork.Add(new Order() {Created = SystemTime.Now, Status = OrderStatus.Created});
		}
	}
}