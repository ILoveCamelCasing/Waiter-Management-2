using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using WaiterManagement.Common.Views;
using WaiterManagement.Web.Models;

namespace WaiterManagement.Web.Infrastructure.ServerProviders
{
	public interface IMenuProvider
	{
		List<MenuItemView> GetMenu();
		List<MenuCategory> GetCategories();
	}

	public class MenuProvider : IMenuProvider
	{
		public List<MenuItemView> GetMenu()
		{
			var elements = new List<MenuItemView>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServerPath"]);
				client.GetAsync("api/menu/getmenu").ContinueWith(taskResponse =>
				{
					var response = taskResponse.Result;
					var jsonString = response.Content.ReadAsStringAsync();
					jsonString.Wait();
					elements = JsonConvert.DeserializeObject<List<MenuItemView>>(jsonString.Result);
				}).Wait();
			}

			return elements;
		}

		public List<MenuCategory> GetCategories()
		{
			return GetMenu().Select(x => new MenuCategory(x.CategoryId, x.CategoryTitle)).DistinctBy(x => x.CategoryId).ToList();
		}
	}
}