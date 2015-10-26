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
		public int CategoryId { get; private set; }
		public Guid CategoryGuid { get; private set; }
		public string Title { get; private set; }
		public string Description { get; private set; }
	}
}