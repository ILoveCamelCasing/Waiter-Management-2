using System.Linq;
using Caliburn.Micro;
using System.Collections.Generic;
using WaiterManagement.Common.Models;
using WaiterManagement.Waiter.Bootstrapper;
using WaiterManagement.Waiter.Connection;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Waiter.ViewModels
{
	public class OrdersViewModel : ViewModelBase
	{
		#region Private Fields
		private OrderModel _selectedAwaitingOrder;
		private OrderModel _selectedAcceptedOrder;
		private IDictionary<int, IEnumerable<AcceptedOrderMenuItemQuantity>> _acceptedOrdersCache;
		#endregion

		#region Dependencies
		private readonly IWaiterConnectionProvider _waiterConnectionProvider;
		#endregion

		#region Constructors
		public OrdersViewModel(IViewModelResolver viewModelResolver, IWaiterAppSubscriber waiterApp, IWaiterConnectionProvider waiterConnectionProvider) : base(viewModelResolver)
		{
			_waiterConnectionProvider = waiterConnectionProvider;

			AwaitingOrders = new BindableCollection<OrderModel>();
			AcceptedOrders = new BindableCollection<OrderModel>();
			SelectedAcceptedOrderMenuItems = new BindableCollection<AcceptedOrderMenuItemQuantity>();

			_acceptedOrdersCache = new Dictionary<int, IEnumerable<AcceptedOrderMenuItemQuantity>>();

			waiterApp.NewOrderHandler += WaiterApp_NotifyNewOrderHandler;
			waiterApp.AcceptedOrderInfoUpdatedHandler += WaiterApp_AcceptedOrderInfoUpdatedHandler;
			waiterApp.OrderWasAcceptedHandler += WaiterApp_OrderWasAcceptedHandler;
		}
		#endregion

		#region Public Properties
		public OrderModel SelectedAwaitingOrder
		{
			get { return _selectedAwaitingOrder; }
			set
			{
				_selectedAwaitingOrder = value;
				NotifyOfPropertyChange(() => SelectedAwaitingOrder);
			}
		}

		public OrderModel SelectedAcceptedOrder
		{
			get { return _selectedAcceptedOrder; }
			set
			{
				_selectedAcceptedOrder = value;
				NotifyOfPropertyChange(() => SelectedAcceptedOrder);
			}
		}

		public BindableCollection<AcceptedOrderMenuItemQuantity> SelectedAcceptedOrderMenuItems
		{ get; private set; }

		public BindableCollection<OrderModel> AwaitingOrders
		{ get; private set; }

		public BindableCollection<OrderModel> AcceptedOrders
		{
			get; private set;
		}
		#endregion

		#region Event Handlers
		public void WaiterApp_NotifyNewOrderHandler(object sender, OrderModel order)
		{
			if (order != null)
				AwaitingOrders.Add(order);
		}


		private void WaiterApp_AcceptedOrderInfoUpdatedHandler(object sender, AcceptedOrderCurrentStateModel orderCurrentState)
		{
			_acceptedOrdersCache[orderCurrentState.OrderId] = orderCurrentState.MenuItems;

			if (orderCurrentState.OrderId == SelectedAcceptedOrder.OrderId)
			{
				SelectedAcceptedOrderMenuItems.Clear();
				SelectedAcceptedOrderMenuItems.AddRange(_acceptedOrdersCache[orderCurrentState.OrderId]);
			}				
		}

		private void WaiterApp_OrderWasAcceptedHandler(object sender, AcceptOrderModel e)
		{
			var orderToRemove = AwaitingOrders.FirstOrDefault(o => o.OrderId == e.OrderId);
			if (orderToRemove != null)
				AwaitingOrders.Remove(orderToRemove);
		}

		public void AcceptedOrderSelectionChanged()
		{
			SelectedAcceptedOrderMenuItems.Clear();

			IEnumerable<AcceptedOrderMenuItemQuantity> menuItems = null;
			if(_acceptedOrdersCache.TryGetValue(SelectedAcceptedOrder.OrderId, out menuItems))
				SelectedAcceptedOrderMenuItems.AddRange(menuItems);
		}

		public void AcceptOrder(OrderModel order)
		{
			AwaitingOrders.Remove(order); //TODO: Remove from db
			AcceptedOrders.Add(order);

			_waiterConnectionProvider.AcceptOrder(order.OrderId);
		}
		#endregion
	}
}