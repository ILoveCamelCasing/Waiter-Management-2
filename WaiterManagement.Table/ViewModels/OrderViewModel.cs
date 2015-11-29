using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using Caliburn.Micro;
using Newtonsoft.Json;
using WaiterManagement.Common.Views;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Table.ViewModels
{
	public class OrderViewModel : ViewModelBase
	{
		#region Private fields

		private bool _isBusy;

		#endregion

		#region Public Properties

		public BindableCollection<MenuItemView> Elements { get; private set; }

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

		#endregion

		public OrderViewModel(IViewModelResolver viewModelResolver) : base(viewModelResolver)
		{
			Elements = new BindableCollection<MenuItemView>();
		}

		#region Ovverrides

		protected override void OnActivate()
		{
			base.OnActivate();

			RefreshMenuAsync();
		}

		private void RefreshMenuAsync()
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