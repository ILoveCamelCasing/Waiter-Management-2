using System.Data.Entity;
using WaiterManagement.Common.Entities;

namespace WaiterManagement.DAL
{
	public class WaiterManagementContext : DbContext
	{
		public WaiterManagementContext() : base("WaiterManagement")
		{
			
		}

		public DbSet<Table> Tables { get; set; } 
	}
}