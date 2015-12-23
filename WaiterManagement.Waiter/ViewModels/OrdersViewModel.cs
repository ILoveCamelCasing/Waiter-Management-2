using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Models;
using WaiterManagement.Waiter.Bootstrapper;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Waiter.ViewModels
{
	public class OrdersViewModel : ViewModelBase
	{
		#region Private Fields
		private OrderModel _selectedAwaitingOrder;
		private OrderModel _selectedAcceptedOrder;
		private IDictionary<int, IEnumerable<MenuItemsQuantity>> _acceptedOrdersCache;
		#endregion

		#region Constructors
		public OrdersViewModel(IViewModelResolver viewModelResolver, IWaiterAppSubscriber waiterApp) : base(viewModelResolver)
		{
			AwaitingOrders = new BindableCollection<OrderModel>();
			AcceptedOrders = new BindableCollection<OrderModel>();
			SelectedAcceptedOrderMenuItems = new BindableCollection<MenuItemsQuantity>();

			_acceptedOrdersCache = new Dictionary<int, IEnumerable<MenuItemsQuantity>>();

			waiterApp.NotifyNewOrder += NotifyNewOrder;
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

		public BindableCollection<MenuItemsQuantity> SelectedAcceptedOrderMenuItems
		{ get; private set; }

		public BindableCollection<OrderModel> AwaitingOrders
		{ get; private set; }

		public BindableCollection<OrderModel> AcceptedOrders
		{
			get; private set;
		}
		#endregion

		#region Event Handlers
		public void NotifyNewOrder(OrderModel order)
		{
			if (order != null)
				AwaitingOrders.Add(order);
		}

		public void AcceptOrder(OrderModel order)
		{
			AwaitingOrders.Remove(order); //TODO: Remove from db
			AcceptedOrders.Add(order);
		}

		public async void AcceptedOrderSelectionChanged()
		{
			var orderId = SelectedAcceptedOrder.OrderId;
			IEnumerable<MenuItemsQuantity> menuItems = null;

			if (!_acceptedOrdersCache.TryGetValue(orderId, out menuItems))
			{
				using (var client = new HttpClient()) //TODO: Koniecznie wywalić do oddzielnej klasy!!
				{
					client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServerPath"]);
					var responseMessage = await client.GetAsync(String.Format("api/waiter/getorder?orderId={0}", orderId));
					var responseString = await responseMessage.Content.ReadAsStringAsync();
					menuItems = JsonConvert.DeserializeObject<List<MenuItemsQuantity>>(responseString);
					_acceptedOrdersCache[orderId] = menuItems;
				}
			}

			SelectedAcceptedOrderMenuItems.Clear();
			SelectedAcceptedOrderMenuItems.AddRange(menuItems);
			NotifyOfPropertyChange(() => SelectedAcceptedOrderMenuItems);
		}
		#endregion
	}
}