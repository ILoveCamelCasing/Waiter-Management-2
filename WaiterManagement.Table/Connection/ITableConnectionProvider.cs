using System.Collections.Generic;
using System.Threading.Tasks;
using WaiterManagement.Table.Model;

namespace WaiterManagement.Table.Connection
{
	public interface ITableConnectionProvider
	{
		Task Connect();
		void Disconnect();

		void MakeNewOrder(IEnumerable<OrderMenuItemModel> orderingElements);
		void OrderMoreItems(IEnumerable<OrderMenuItemModel> orderingElements);
		void CallWaiter();
	}
}