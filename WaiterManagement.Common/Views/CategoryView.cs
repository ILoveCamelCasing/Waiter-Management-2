using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.Common.Views
{
	[Table("CategoriesView")]
	public class CategoryView : IView
	{
		[Key]
		public int CategoryId { get; set; }
		public Guid CategoryGuid { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
	}
}