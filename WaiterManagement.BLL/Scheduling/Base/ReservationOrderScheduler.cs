using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using NLog;
using WaiterManagement.BLL.Events.Base;
using WaiterManagement.BLL.Events.Concrete.Service;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Scheduling.Base
{
	public class ReservationOrderScheduler : IReservationOrderScheduler
	{
		#region Private Const Fields
		private const int SchedulingInterval = 10000;
		private const int ReservationInterval = 15000;
		#endregion

		#region Private Fields
		private Timer _timer;
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		#endregion

		#region Dependencies

		private readonly IUnitOfWork _unitOfWork;
		private readonly IEventBus _eventBus;
		#endregion

		#region Constructors

		public ReservationOrderScheduler(IUnitOfWork unitOfWork, IEventBus eventBus)
		{
			if (unitOfWork == null)
				throw new ArgumentNullException(nameof(unitOfWork));
			if (eventBus == null)
				throw new ArgumentNullException(nameof(eventBus));

			_unitOfWork = unitOfWork;
			_eventBus = eventBus;

			_timer = new Timer(OnTimerTick, null, SchedulingInterval, SchedulingInterval);
		}

		#endregion


		#region Event Handlers
		private void OnTimerTick(object state)
		{
			try
			{
				var awaitingOrders = _unitOfWork.GetWhere<ReservationOrder>(resO => resO.Status == ReservationOrderStatus.Created && DbFunctions.DiffMilliseconds(resO.ReservationTime, DateTime.Now) < ReservationInterval).ToList();
				if (!awaitingOrders.Any())
					return;

				foreach (var awaitingOrder in awaitingOrders)
				{
					var menuItems =
						_unitOfWork.GetWhere<ReservationMenuItemQuantity>(miq => miq.ReservationOrder.Id == awaitingOrder.Id).ToList();

					foreach(var menuItem in menuItems)
						_unitOfWork.Load(menuItem, mi => mi.Item);

					_unitOfWork.Load(awaitingOrder, o => o.Table);

					_eventBus.PublishEvent(new ReservationOrderScheduled()
					{
						UnlockCode = awaitingOrder.UnlockCode,
						MenuItems = menuItems,
						TableLogin = awaitingOrder.Table.Title
					});

					awaitingOrder.Status = ReservationOrderStatus.Finished;

					_unitOfWork.Commit();
				}

				//_eventBus.PublishEvent(new ReservationOrderScheduled()
				//{
				//	UnlockCode = "123",
				//	Order = new Order()
				//	{
				//		Table = new Table()
				//		{
				//			Title = "table"
				//		}
				//	}
				//});
			}
			catch (Exception ex)
			{
				_logger.Fatal("Scheduling reservation order failed. Exception {0}. Message {1}. Stacktrace {2}", ex.GetType().FullName, ex.Message, ex.StackTrace);
				_unitOfWork.Revert();

				throw;
			}

			_eventBus.HandleEvents();
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

