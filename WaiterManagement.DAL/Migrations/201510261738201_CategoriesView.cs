namespace WaiterManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoriesView : DbMigration
    {
		public override void Up()
		{
			Sql(DbMigrationExtensions.CreateViewQuery("CategoriesView",
				"SELECT [Id] as CategoryId, [CommonId] as CategoryGuid, [Title], [Description] From Categories WHERE IsNewest=1 AND IsDeleted=0"));

		}

		public override void Down()
		{
			Sql(DbMigrationExtensions.DropViewQuery("CategoriesView"));
		}
    }
}
