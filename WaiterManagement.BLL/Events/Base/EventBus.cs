using System;
using System.Collections.Generic;
using System.Linq;

namespace WaiterManagement.BLL.Events.Base
{
	public class EventBus : IEventBus
	{
		private readonly Func<Type, IEnumerable<IHandleEvent>> _handlersFactory;
		private readonly Stack<Action> _eventsToHandling;

		public EventBus(Func<Type, IEnumerable<IHandleEvent>> handlersFactory)
		{
			_handlersFactory = handlersFactory;
			_eventsToHandling = new Stack<Action>();
		}

		public void PublishEvent<T>(T e) where T : IEvent
		{
			var handlers = _handlersFactory(typeof(T))
				.Cast<IHandleEvent<T>>();

			foreach (var handler in handlers)
			{
				_eventsToHandling.Push(() => handler.Handle(e));
			}
		}

		public void HandleEvents()
		{
			while (_eventsToHandling.Any())
			{
				_eventsToHandling.Pop().Invoke();
			}
		}
	}
}