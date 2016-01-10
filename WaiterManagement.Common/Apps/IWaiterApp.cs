using System.Collections;
using System.Collections.Generic;
using WaiterManagement.Common.Models;

namespace WaiterManagement.Common.Apps
{
	public interface IWaiterApp
	{
		/// <summary>
		/// Zdarzenie przyjęcia zamówienia przez innego kelnera
		/// </summary>
		/// <param name="order"></param>
		void OrderWasAccepted(AcceptOrderModel order);
		/// <summary>
		/// Zdarzenie złożenia nowego zamówienia
		/// </summary>
		/// <param name="order"></param>
		void NewOrderMade(OrderModel order);
		/// <summary>
		/// Zdarzenie emitowane przez serwis po zalogowaniu kelnera. Przesyła wszystkie aktualnie oczekujące zamówienia
		/// </summary>
		/// <param name="orders"></param>
		void OrdersAwaiting(IEnumerable<OrderModel> orders);
		/// <summary>
		/// Zdarzenie zmiany w zamówieniu przyjętym przez kelnera
		/// <remarks>
		/// Emitowane również zaraz po przyjęciu zamówienia
		/// </remarks>
		/// </summary>
		/// <param name="acceptedOrder"></param>
		void AcceptedOrderInfoUpdated(AcceptedOrderCurrentStateModel acceptedOrder);
		void CallWaiter(string tableLogin);
	}
}