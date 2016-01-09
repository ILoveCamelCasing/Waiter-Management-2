﻿namespace WaiterManagement.Common.Models
{
	public class AcceptedOrderMenuItemQuantity
	{
		public int MenuItemQuantityId { get; set; }
		public AcceptedOrderMenuItem MenuItem { get; set; }
		public int Quantity { get; set; }
		public bool Ready { get; set; }
	}
}
