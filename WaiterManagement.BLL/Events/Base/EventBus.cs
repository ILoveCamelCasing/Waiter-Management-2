using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace WaiterManagement.BLL.Events.Base
{
	public class EventBus : IEventBus
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		private readonly Func<Type, IEnumerable<IHandleEvent>> _handlersFactory;
		private readonly Stack<IEventCahe> _eventsToHandling;

		public EventBus(Func<Type, IEnumerable<IHandleEvent>> handlersFactory)
		{
			_handlersFactory = handlersFactory;
			_eventsToHandling = new Stack<IEventCahe>();
		}

		public void PublishEvent<T>(T e) where T : IEvent
		{
			var handlers = _handlersFactory(typeof(T))
				.Cast<IHandleEvent<T>>();

			foreach (var handler in handlers)
			{
				_eventsToHandling.Push(new EventCache<T>(e,handler));
			}
		}

		public void HandleEvents()
		{
			while (_eventsToHandling.Any())
			{
				var eventCache = _eventsToHandling.Pop();
				try
				{
					eventCache.Handle();
				}
				catch (Exception ex)
				{
					Logger.Error("Event {0} failed. Exception {1}. Message {2}. StackTrace {3}.", eventCache.EventType.FullName,ex.GetType().FullName,ex.Message,ex.StackTrace);
				}
			}
		}

		private interface IEventCahe
		{
			Type EventType { get; }
			Type HandleType { get; }
			void Handle();
		}

		private class EventCache<T> : IEventCahe where T : IEvent
		{
			private readonly T _event;
			private readonly IHandleEvent<T> _handler;

			public Type EventType { get { return _event.GetType(); } }
			public Type HandleType { get { return _handler.GetType(); } }

			public EventCache(T @event, IHandleEvent<T> handler)
			{
				_event = @event;
				_handler = handler;
			}

			public void Handle()
			{
				_handler.Handle(_event);
			}
		}
	}
}