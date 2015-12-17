using System;

namespace WaiterManagement.BLL.Commands.Base
{
	public class CommandBus : ICommandBus
	{
		private readonly Func<Type, IHandleCommand> _handlersFactory;

		public CommandBus(Func<Type, IHandleCommand> handlersFactory)
		{
			_handlersFactory = handlersFactory;
		}

		public void SendCommand<T>(T command) where T : ICommand
		{
			var handler = (IHandleCommand<T>)_handlersFactory(typeof(T));
			handler.Handle(command);
			handler.UnitOfWork.Commit();
			handler.EventBus.HandleEvents();
		}
	}
}