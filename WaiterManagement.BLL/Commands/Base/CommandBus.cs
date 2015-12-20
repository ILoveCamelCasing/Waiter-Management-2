using System;
using NLog;

namespace WaiterManagement.BLL.Commands.Base
{
	public class CommandBus : ICommandBus
	{
		private static Logger _logger = LogManager.GetCurrentClassLogger();

		private readonly Func<Type, IHandleCommand> _handlersFactory;

		public CommandBus(Func<Type, IHandleCommand> handlersFactory)
		{
			_handlersFactory = handlersFactory;
		}

		public void SendCommand<T>(T command) where T : ICommand
		{
			var handler = (IHandleCommand<T>)_handlersFactory(typeof(T));
			try
			{
				handler.Handle(command);
				handler.UnitOfWork.Commit();
			}
			catch (Exception ex)
			{
				_logger.Fatal("Command {0} failed. Exception {1}. Message {2}. Stacktrace {3}",command.GetType().FullName, ex.GetType().FullName, ex.Message, ex.StackTrace);
				throw;
			}
			handler.EventBus.HandleEvents();
		}
	}
}