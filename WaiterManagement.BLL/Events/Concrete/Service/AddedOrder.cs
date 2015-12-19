using WaiterManagement.BLL.Events.Base;
using WaiterManagement.Common.Apps;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Events.Concrete.Service
{
	public class AddedOrder : IEvent
	{
		public Order Order { get; set; }
		public ITableApp Table { get; set; }
		public string TableTitle { get; set; }
	}
}