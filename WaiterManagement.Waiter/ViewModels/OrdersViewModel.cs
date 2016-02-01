using System.Linq;
using Caliburn.Micro;
using System.Collections.Generic;
using WaiterManagement.Common.Models;
using WaiterManagement.Waiter.Bootstrapper;
using WaiterManagement.Waiter.Connection;
using WaiterManagement.Wpf.MVVM.Abstract;
using System;
using System.Windows;
using WaiterManagement.Wpf.Controls;
using Action = System.Action;

namespace WaiterManagement.Waiter.ViewModels
{
	public class OrdersViewModel : ViewModelBase
	{
		#region Private Fields
		private OrderModel _selectedAwaitingOrder;
		private OrderModel _selectedAcceptedOrder;
		private IDictionary<int, IEnumerable<AcceptedOrderMenuItemQuantity>> _acceptedOrdersCache;
		private decimal _selectedAcceptedOrderTotalPrice;
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
			TablesRequiringAssistance = new BindableCollection<String>();

			_acceptedOrdersCache = new Dictionary<int, IEnumerable<AcceptedOrderMenuItemQuantity>>();

			waiterApp.NewOrderHandler += WaiterApp_NotifyNewOrderHandler;
			waiterApp.AcceptedOrderInfoUpdatedHandler += WaiterApp_AcceptedOrderInfoUpdatedHandler;
			waiterApp.OrderWasAcceptedHandler += WaiterApp_OrderWasAcceptedHandler;
			waiterApp.CallWaiterHandler += WaiterApp_CallWaiterHandler;
			waiterApp.OrdersAwaitingHandler += WaiterApp_OrdersAwaitingHandler;

			_waiterConnectionProvider.UpdateAfterLogin();
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
				if (_selectedAcceptedOrder == value)
					return;

				_selectedAcceptedOrder = value;
				UpdateSelectedAcceptedOrderMenuItems();

				NotifyOfPropertyChange(() => SelectedAcceptedOrder);
			}
		}

		public decimal SelectedAcceptedOrderTotalPrice
		{
			get
			{
				return _selectedAcceptedOrderTotalPrice;
			}
			set
			{
				_selectedAcceptedOrderTotalPrice = value;
				NotifyOfPropertyChange(() => SelectedAcceptedOrderTotalPrice);
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

		public BindableCollection<string> TablesRequiringAssistance
		{
			get; private set;
		}

		public bool CanEndOrder
		{
			get { return SelectedAcceptedOrderMenuItems.Any() && SelectedAcceptedOrderMenuItems.All(mi => mi.Ready); }
		}
		#endregion

		#region Event Handlers
		public void WaiterApp_NotifyNewOrderHandler(object sender, OrderModel order)
		{
			if (order != null && AwaitingOrders.All(o => o.OrderId != order.OrderId))
				AwaitingOrders.Add(order);
		}

		private void WaiterApp_OrdersAwaitingHandler(object sender, IEnumerable<OrderModel> orders)
		{
			if(orders != null)
				AwaitingOrders.AddRange(orders.Where(o => AwaitingOrders.All(ao => ao.OrderId != o.OrderId)));
		}

		private void WaiterApp_AcceptedOrderInfoUpdatedHandler(object sender, AcceptedOrderCurrentStateModel orderCurrentState)
		{
			_acceptedOrdersCache[orderCurrentState.OrderId] = orderCurrentState.MenuItems;

			if (orderCurrentState.OrderId == SelectedAcceptedOrder.OrderId)
			{
				SelectedAcceptedOrderMenuItems.Clear();
				SelectedAcceptedOrderMenuItems.AddRange(_acceptedOrdersCache[orderCurrentState.OrderId]);
				RecalculateTotalPrice();
			}
			
			NotifyOfPropertyChange(() => CanEndOrder);				
		}

		private void WaiterApp_OrderWasAcceptedHandler(object sender, AcceptOrderModel acceptedOrder)
		{
			var orderToRemove = AwaitingOrders.FirstOrDefault(o => o.OrderId == acceptedOrder.OrderId);
			if (orderToRemove != null)
				AwaitingOrders.Remove(orderToRemove);
		}

		private void WaiterApp_CallWaiterHandler(object sender, string callingTable)
		{
			if(!TablesRequiringAssistance.Contains(callingTable))
				TablesRequiringAssistance.Add(callingTable);
		}

		public void AcceptOrder(OrderModel order)
		{
			AwaitingOrders.Remove(order);
			AcceptedOrders.Add(order);

			_waiterConnectionProvider.AcceptOrder(order.OrderId);
			NotifyOfPropertyChange(() => CanEndOrder);
		}

		public void EndOrder(OrderModel order)
		{
			_waiterConnectionProvider.EndOrder(order.OrderId, false, string.Empty);
			UpdateAfterEndOrder(order.OrderId);
		}

		public void CancelOrder(OrderModel order)
		{
			ModernInputDialogMessageBoxResult result = null;
			Action showMessageAction = () =>
			{
				result = ModernInputDialog.ShowInputMessage("Cancelling reason: ", "Cancelling order", MessageBoxButton.OK,
					Application.Current.MainWindow);
			};

			var dispatcher = Application.Current.Dispatcher;
			if(dispatcher == null || dispatcher.CheckAccess())
				showMessageAction.Invoke();
			else
				dispatcher.Invoke(showMessageAction);

			_waiterConnectionProvider.EndOrder(order.OrderId, true, result.Input);
			UpdateAfterEndOrder(order.OrderId);
		}

		public void MarkAssistanceRequirementAsSeen(string tableLogin)
		{
			TablesRequiringAssistance.Remove(tableLogin);
		}

		public void OrderItemStateChanged(AcceptedOrderMenuItemQuantity orderItem)
		{
			_waiterConnectionProvider.ChangeOrderItemState(_selectedAcceptedOrder.OrderId, orderItem);

			NotifyOfPropertyChange(() => CanEndOrder);
		}
		#endregion

		#region Private Methods
		private void UpdateSelectedAcceptedOrderMenuItems()
		{
			SelectedAcceptedOrderMenuItems.Clear();

			if (SelectedAcceptedOrder == null)
				return;

			IEnumerable<AcceptedOrderMenuItemQuantity> menuItems = null;
			if (_acceptedOrdersCache.TryGetValue(SelectedAcceptedOrder.OrderId, out menuItems))
				SelectedAcceptedOrderMenuItems.AddRange(menuItems);

			RecalculateTotalPrice();
		}

		private void UpdateAfterEndOrder(int orderId)
		{
			_acceptedOrdersCache.Remove(orderId);

			SelectedAcceptedOrderMenuItems.Clear();
			AcceptedOrders.Remove(SelectedAcceptedOrder);

			RecalculateTotalPrice();
		}

		private void RecalculateTotalPrice()
		{
			if (SelectedAcceptedOrderMenuItems == null || !SelectedAcceptedOrderMenuItems.Any())
				SelectedAcceptedOrderTotalPrice = 0;
			else
			{
				SelectedAcceptedOrderTotalPrice = SelectedAcceptedOrderMenuItems.Select(mi => mi.MenuItem.Price*mi.Quantity).Sum();
			}
		}
		#endregion

		#region Overrides
		protected override void OnDeactivate(bool close)
		{
			base.OnDeactivate(close);

			if (close)
			{
				var cachedOrderIds = new List<int>();
				foreach (var cachedOrder in _acceptedOrdersCache)
				{
					_waiterConnectionProvider.EndOrder(cachedOrder.Key, true, "Waiter has logged out.");
					cachedOrderIds.Add(cachedOrder.Key);
				}

				foreach(var orderId in cachedOrderIds)
					UpdateAfterEndOrder(orderId);

				_waiterConnectionProvider.Disconnect();
			}
		}
		#endregion
	}
}