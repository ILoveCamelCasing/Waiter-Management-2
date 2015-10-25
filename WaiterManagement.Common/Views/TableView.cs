using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.Common.Views
{
	[Table("TablesView")]
	public class TableView : IView
	{
		[Key]
		public int TableId { get; private set; }
		public Guid TableGuid { get; private set; }
		public string Title { get; private set; }
		public string Description { get; private set; }
	}
}