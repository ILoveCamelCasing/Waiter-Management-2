namespace WaiterManagement.BLL.Events.Base
{
	public interface IEventBus
	{
		void PublishEvent<T>(T e) where T : IEvent;
		void HandleEvents();
	}
}