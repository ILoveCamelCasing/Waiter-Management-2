using System.Data.Entity;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Views;
using WaiterManagement.DAL.Migrations;

namespace WaiterManagement.DAL
{
	public class WaiterManagementContext : DbContext
	{
		public WaiterManagementContext()
			: base("WaiterManagement")
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<WaiterManagementContext, Configuration>());
		}

		#region Entities

		public DbSet<Table> Tables { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Waiter> Waiters { get; set; }
		public DbSet<MenuItem> MenuItems { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<ActiveUser> ActiveUsers { get; set; }
		#endregion

		#region Views

		public DbSet<TableView> TablesView { get; set; }
		public DbSet<CategoryView> CategoriesView { get; set; }
		public DbSet<WaiterView> WaitersView { get; set; }
		public DbSet<MenuItemView> MenuItemsView { get; set; }

		#endregion
	}
}