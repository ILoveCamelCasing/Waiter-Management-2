using Ninject;
using WaiterManagement.BLL.Events.Base;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Base
{
	public abstract class Handler : IHandleCommand
	{
		[Inject]
		public IUnitOfWork UnitOfWork { get; set; }

		[Inject]
		public IEventBus EventBus { get; set; }
	}
}