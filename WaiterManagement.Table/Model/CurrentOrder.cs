using WaiterManagement.Table.Bootstrapper;

namespace WaiterManagement.Table.Model
{
	class CurrentOrder : ICurrentOrder
	{
		public int CurrentOrderId { get; private set; }

		public CurrentOrder(ITableAppSubscriber tableApp)
		{
			tableApp.SendOrderIdEvent += (sender, id) => CurrentOrderId = id;
		}
	}
}