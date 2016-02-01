using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.Common.Views
{
	[Table("MenuItemsView")]
	public class MenuItemView : IView
	{
		[Key]
		public int MenuItemId { get; set; }
		public Guid MenuItemGuid { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int CategoryId { get; set; }
		public string CategoryTitle { get; set; }
		public decimal Price { get; set; }
	}
}