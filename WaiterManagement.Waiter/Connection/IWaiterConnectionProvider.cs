﻿using System.Threading.Tasks;

namespace WaiterManagement.Waiter.Connection
{
	public interface IWaiterConnectionProvider
	{
		Task Connect();

		void AcceptOrder(int orderId);
	}
}