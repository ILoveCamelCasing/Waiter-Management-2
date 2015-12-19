using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Windows;
using Caliburn.Micro;
using Newtonsoft.Json;
using WaiterManagement.Common.Views;
using WaiterManagement.Table.Bootstrapper;
using WaiterManagement.Table.Connection;
using WaiterManagement.Table.Model;
using WaiterManagement.Wpf.MVVM.Abstract;

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

		public string OrderText { get { return "Send new order"; } }

		public string Message
		{
			get { return _message; }
			set
			{
				_message = value;
				NotifyOfPropertyChange(() => Message);
			}
		}

		#endregion

		#region Constructor

		public OrderViewModel(IViewModelResolver viewModelResolver, ITableConnectionProvider tableConnectionProvider, ITableAppSubscriber tableAppSubscriber) : base(viewModelResolver)
		{
			_tableConnectionProvider = tableConnectionProvider;

			tableAppSubscriber.Notify += message => Application.Current.Dispatcher.Invoke(() => Message = message);

			Elements = new BindableCollection<MenuItemView>();
			AddedElements = new BindableCollection<OrderMenuItemModel>();
		}

		#endregion

		#region Public methods

		public void Order()
		{
			_tableConnectionProvider.MakeNewOrder(AddedElements.Where(x => x.Ordered == false));
		}

		public void AddNewItem(MenuItemView addingMenuItem)
		{
			var element = AddedElements.FirstOrDefault(x => x.Id == addingMenuItem.MenuItemId);
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

		#endregion
	}
}