using System.Collections.Generic;
using WaiterManagement.Table.Model;

namespace WaiterManagement.Table.Connection
{
	public interface ITableConnectionProvider
	{
		void MakeNewOrder(IEnumerable<OrderMenuItemModel> orderingElements);
	}
}