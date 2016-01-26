namespace WaiterManagement.Common.Models
{
	public class EndOrderModel
	{
		public int OrderId { get; set; }
		public bool OrderCancelled { get; set; }
		public string OrderCancelledReason { get; set; }
	}
}
