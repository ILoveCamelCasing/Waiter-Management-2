using WaiterManagement.BLL.Events.Base;

namespace WaiterManagement.BLL.Events.Concrete.Service
{
	public class WaiterLoggedIn : IEvent
	{
		public string WaiterLogin { get; set; }
	}
}
