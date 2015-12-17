using WaiterManagement.BLL.Events.Base;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Base
{
	public interface IHandleCommand
	{
		IUnitOfWork UnitOfWork { get; }
		IEventBus EventBus { get; }
	}

	public interface IHandleCommand<in TCommand> : IHandleCommand
		where TCommand : ICommand
	{
		void Handle(TCommand command);
	}
}