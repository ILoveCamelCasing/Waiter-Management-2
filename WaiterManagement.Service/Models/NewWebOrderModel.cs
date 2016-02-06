using System;

namespace WaiterManagement.Service.Models
{
	public class NewWebOrderModel
	{
		public string Login { get; set; }
		public DateTime OrderDate { get; set; }
		public NewWebOrderItemModel[] Items { get; set; }
	}

	public class NewWebOrderItemModel
	{
		public int ItemId { get; set; }
		public int Quantity { get; set; }
	}
}