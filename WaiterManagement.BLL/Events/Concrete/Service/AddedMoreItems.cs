using WaiterManagement.BLL.Events.Base;

namespace WaiterManagement.BLL.Events.Concrete.Service
{
	public class AddedMoreItems : IEvent
	{
		public int OrderId { get; set; }
	}
}