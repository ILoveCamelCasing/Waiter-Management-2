using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Windows;
using Caliburn.Micro;
using FirstFloor.ModernUI.Windows.Controls;
using Newtonsoft.Json;
using WaiterManagement.Common.Models;
using WaiterManagement.Common.Views;
using WaiterManagement.Table.Bootstrapper;
using WaiterManagement.Table.Connection;
using WaiterManagement.Table.Model;
using WaiterManagement.Wpf.MVVM.Abstract;
using Action = System.Action;

namespace WaiterManagement.Table.ViewModels
{
	public class OrderViewModel : ViewModelBase
	{
		#region Dependencies

		private readonly ITableConnectionProvider _tableConnectionProvider;

		#endregion

		#region Private fields

		private bool _isBusy;
		private string _message;
		private bool _isSomethingOrdered;
		private double _totalPrice;
		#endregion

		#region Public Properties

		public BindableCollection<MenuItemView> Elements { get; private set; }
		public BindableCollection<OrderMenuItemModel> AddedElements { get; private set; }

		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}
			set
			{
				_isBusy = value;
				NotifyOfPropertyChange(() => IsBusy);
			}
		}

		public bool IsSomethingOrdered { get { return _isSomethingOrdered; } set { _isSomethingOrdered = value; NotifyOfPropertyChange(() => OrderText); }}
		public string OrderText { get { return IsSomethingOrdered ? "Order more items" : "Send new order"; } }
		
		public string Message
		{
			get { return _message; }
			set
			{
				_message = value;
				NotifyOfPropertyChange(() => Message);
			}
		}

		public double TotalPrice
		{
			get
			{
				return _totalPrice;
			}
			set
			{
				_totalPrice = value;
				NotifyOfPropertyChange(() => TotalPrice);
			}
	}
		#endregion

		#region Constructor

		public OrderViewModel(IViewModelResolver viewModelResolver, ITableConnectionProvider tableConnectionProvider, ITableAppSubscriber tableAppSubscriber) : base(viewModelResolver)
		{
			_tableConnectionProvider = tableConnectionProvider;

			tableAppSubscriber.NotifyEvent += (sender, message) => Application.Current.Dispatcher.Invoke(() => Message = message);
			tableAppSubscriber.NotifyOrderEndedEvent += TableAppSubscriber_NotifyOrderEndedEvent;
			tableAppSubscriber.OrderItemStateChangedEvent += TableAppSubscriber_OrderItemStateChangedEvent;

			Elements = new BindableCollection<MenuItemView>();
			AddedElements = new BindableCollection<OrderMenuItemModel>();
		}
		#endregion

		#region Public methods

		public void Order()
		{
			if (IsSomethingOrdered)
			{
				var notOrderedItems = AddedElements.Where(x => x.Ordered == false).ToArray();
				_tableConnectionProvider.OrderMoreItems(notOrderedItems);
				foreach (var item in notOrderedItems)
				{
					//Zakomentowane, aby lepiej odzwierciedlić aktualny stan zamówienia. Ponadto, łatwiej tak będzie zaznaczyć konkretny element zamówienia jako gotowy.

					//AddedElements.Remove(item);
					//var sameTypeElement = AddedElements.FirstOrDefault(x => x.Title == item.Title);
					//if (sameTypeElement != null)
					//	sameTypeElement.Quantities += item.Quantities;
					//else
					//{
						item.Ordered = true;
						//AddedElements.Add(item);
					//}
				}
			}
			else
			{
				_tableConnectionProvider.MakeNewOrder(AddedElements);
				foreach (var element in AddedElements)
				{
					element.Ordered = true;
				}
				IsSomethingOrdered = true;
			}

			AddedElements.Refresh();
		}

		public void AddNewItem(MenuItemView addingMenuItem)
		{
			var element = AddedElements.FirstOrDefault(x => x.Id == addingMenuItem.MenuItemId && !x.Ordered);
			if (element != null)
			{
				element.Quantities++;
				AddedElements.Refresh();
			}
			else
			{
				AddedElements.Add(new OrderMenuItemModel()
				{
					Id = addingMenuItem.MenuItemId,
					Ordered = false,
					Quantities = 1,
					Title = addingMenuItem.Title
				});
			}

			//TotalPrice += addingMenuItem.Price; TODO: Odkomentować
		}

		public void CallWaiter()
		{
			try
			{
				_tableConnectionProvider.CallWaiter();
			}
			catch (Exception ex)
			{
				Message = "Error emerged. No waiter called.";
			}
		}

		#endregion

		#region Event Handlers
		private void TableAppSubscriber_OrderItemStateChangedEvent(object sender, OrderItemState e)
		{
			var element =
				AddedElements.FirstOrDefault(omi => omi.Id == e.MenuItemId && omi.Quantities == e.Quantity && omi.Ready != e.Ready); //Może zamiast sprawdzania po menuitemid i quantities (nie gwarantuje unikalności), lepiej przekazywać menuitemquantityId i po tym sprawdzać ?

			if (element != null)
				element.Ready = e.Ready;

			AddedElements.Refresh();
		}

		private void TableAppSubscriber_NotifyOrderEndedEvent(object sender, EndOrderModel endOrderModel)
		{
			if (!IsSomethingOrdered) //TODO: Z jakiegoś powodu wykonuje się dwukrotnie...
				return;

			var dispatcher = Application.Current.Dispatcher;
			var endOrderMessage = endOrderModel.OrderCancelled
				? $"Your order was cancelled: {endOrderModel.OrderCancelledReason}"
				: "Your order was processed successfully.";

			Action showMessageAction = () =>
			{
				ModernDialog.ShowMessage(endOrderMessage, "Order was closed", MessageBoxButton.OK, Application.Current.MainWindow);
			};

			if(dispatcher == null || dispatcher.CheckAccess())
				showMessageAction.Invoke();
			else
				dispatcher.Invoke(showMessageAction);
			
			IsSomethingOrdered = false;
			AddedElements.Clear();
		}
		#endregion

		#region Ovverrides

		protected override void OnActivate()
		{
			base.OnActivate();

			RefreshMenu();
		}

		private void RefreshMenu()
		{
			IsBusy = true;
			Elements.Clear();
			var newElements = new List<MenuItemView>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServerPath"]);
				client.GetAsync("api/menu/getmenu").ContinueWith(taskResponse =>
				{
					var response = taskResponse.Result;
					var jsonString = response.Content.ReadAsStringAsync();
					jsonString.Wait();
					newElements = JsonConvert.DeserializeObject<List<MenuItemView>>(jsonString.Result);
				}).Wait();
			}

			Elements.AddRange(newElements);

			NotifyOfPropertyChange(() => Elements);
			IsBusy = false;
		}

		protected override void OnDeactivate(bool close)
		{
			base.OnDeactivate(close);

			if (close)
			{
				_tableConnectionProvider.Disconnect();
			}
		}

		#endregion
	}
}