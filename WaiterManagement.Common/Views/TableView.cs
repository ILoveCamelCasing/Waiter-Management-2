using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.Common.Views
{
	[Table("TablesView")]
	public class TableView : ILoginableView
	{
		[Key]
		public int TableId { get; set; }
		public Guid TableGuid { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Login { get; set; }
		public string SecondHash { get; set; }
		public Guid UserId { get; set; }
	}
}