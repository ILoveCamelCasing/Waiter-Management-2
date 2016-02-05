using System;
using System.Linq;
using System.Threading;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.BLL.Scheduling.Base
{
	public class ReservationOrderScheduler : Handler, IDisposable
	{
		#region Private Const Fields
		private const int SchedulingInterval = 1000;
		private const int ReservationInterval = 1000;
		#endregion

		#region Private Fields
		private Timer _timer;
		#endregion

		#region Constructors

		public ReservationOrderScheduler()
		{
			_timer = new Timer(OnTimerTick, null, 0, SchedulingInterval);
		}
		#endregion


		#region Event Handlers
		private void OnTimerTick(object state)
		{
			var awaitingOrders = UnitOfWork.GetWhere<ReservationOrder>(resO => resO.Status == ReservationOrderStatus.Created && (resO.ReservationTime - DateTime.Now).TotalMilliseconds < ReservationInterval);

			if (awaitingOrders == null || !awaitingOrders.Any())
				return;

			foreach (var awaitingOrder in awaitingOrders)
			{
				UnitOfWork.Load(awaitingOrder, o => o.Order);
				UnitOfWork.Load(awaitingOrder.Order, o => o.Table);

				EventBus.PublishEvent(new ReservationOrderScheduled()
				{
					Order = awaitingOrder.Order
				});

				awaitingOrder.Status = ReservationOrderStatus.Finished;
			}

		}
		#endregion


		#region IDisposable
		public void Dispose()
		{
			if (_timer != null)
			{
				_timer.Dispose();
				_timer = null;
			}
		}
		#endregion
	}
}

