using System.Data.Entity;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Views;

namespace WaiterManagement.DAL
{
	public class WaiterManagementContext : DbContext
	{
		public WaiterManagementContext() : base("WaiterManagement")
		{

		}

		#region Entities

		public DbSet<Table> Tables { get; set; }
		public DbSet<Category> Categories { get; set; }

	#endregion

		#region Views

		public DbSet<TableView> TablesView { get; set; } 

		#endregion
	}
}