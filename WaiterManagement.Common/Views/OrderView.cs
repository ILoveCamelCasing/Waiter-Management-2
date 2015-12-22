using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.Common.Views
{
	[Table("OrdersView")]
	public class OrderView : IView
	{
		[Key]
		public int OrderId { get; set; }
		public DateTime Created { get; set; }
		public OrderStatus Status { get; set; }
		public string Comment { get; set; }
		public int Quantity { get; set; }
		public string MenuItem { get; set; }
		public int WaiterId { get; set; }
		public string WaiterLogin { get; set; }
		public int TableId { get; set; }
		public string TableTitle { get; set; }
	}
}