﻿using System.Data.Entity;
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

		// Manager entities
		public DbSet<Table> Tables { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Waiter> Waiters { get; set; }
		public DbSet<MenuItem> MenuItems { get; set; }

		// Access entities
		public DbSet<User> Users { get; set; }
		public DbSet<ActiveUser> ActiveUsers { get; set; }

		// Order entities
		public DbSet<Order> Orders { get; set; }
		public DbSet<MenuItemsQuantity> MenuItemsQuantities { get; set; }

		// Web entities
		public DbSet<WebClient> WebClients { get; set; }
		public DbSet<ReservationOrder> ReservationOrders { get; set; }
		public DbSet<ReservationMenuItemQuantity> ReservationMenuItemQuantities { get; set; }

		#endregion

		#region Views

		public DbSet<TableView> TablesView { get; set; }
		public DbSet<CategoryView> CategoriesView { get; set; }
		public DbSet<WaiterView> WaitersView { get; set; }
		public DbSet<MenuItemView> MenuItemsView { get; set; }
		public DbSet<AuthenticatedUsersView> AuthenticatedUsersView { get; set; }
		public DbSet<OrderView> OrdersView { get; set; }
		public DbSet<WebClientView> WebClientsView { get; set; }
		public DbSet<ReservationView> ReservationsView { get; set; } 

		#endregion
	}
}