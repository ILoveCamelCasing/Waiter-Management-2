namespace WaiterManagement.Common.Models
{
	public class OrderModel
	{
		public string TableTitle { get; set; }
		public int OrderId { get; set; }

		public string OrderIdHeaderString
		{
			get
			{
				return $"Order #{OrderId}";
			}
		}
	}
}