using System;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
	public class ReservationOrder : NonVersionableEntity
	{
		public DateTime Created { get; set; }
		public DateTime ReservationTime { get; set; }
		public string UnlockCode { get; set; }
		public WebClient Client { get; set; }
		public ReservationOrderStatus Status { get; set; }
		public string Comment { get; set; }
		public Order Order { get; set; }
		public Table Table { get; set; }
	}

	public enum ReservationOrderStatus
	{
		Created = 1,
		Cancelled,
		Finished
	}
}