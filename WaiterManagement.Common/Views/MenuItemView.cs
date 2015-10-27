using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.Common.Views
{
	[Table("MenuItemsView")]
	public class MenuItemView : IView
	{
		[Key]
		public int MenuItemId { get; private set; }
		public Guid MenuItemGuid { get; private set; }
		public string Title { get; private set; }
		public string Description { get; private set; }
		public int CategoryId { get; private set; }
		public string CategoryTitle { get; private set; }
	}
}