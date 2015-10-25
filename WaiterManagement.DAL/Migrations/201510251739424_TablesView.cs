namespace WaiterManagement.DAL.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class TablesView : DbMigration
	{
		public override void Up()
		{
			Sql(DbMigrationExtensions.CreateViewQuery("TablesView",
				"SELECT [Id] as TableId, [CommonId] as TableGuid, [Title], [Description] From Tables WHERE IsNewest=1 AND IsDeleted=0"));

		}

		public override void Down()
		{
			Sql(DbMigrationExtensions.DropViewQuery("TablesView"));
		}
	}
}
