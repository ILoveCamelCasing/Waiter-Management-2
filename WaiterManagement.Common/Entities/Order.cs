using System;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
	public class Order : NonVersionableEntity
	{
		public DateTime Created { get; set; }
		public OrderStatus Status { get; set; }
		public string Comment { get; set; }
		public Table Table { get; set; }
		public Waiter Waiter { get; set; }
	}
}