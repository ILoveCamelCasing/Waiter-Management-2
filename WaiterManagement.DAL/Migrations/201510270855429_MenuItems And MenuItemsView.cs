namespace WaiterManagement.DAL.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class MenuItemsAndMenuItemsView : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.MenuItems",
				c => new
					{
						Id = c.Int(nullable: false, identity: true),
						Title = c.String(),
						Description = c.String(),
						CommonId = c.Guid(nullable: false),
						Created = c.DateTime(nullable: false),
						Modified = c.DateTime(),
						Version = c.Int(nullable: false),
						IsNewest = c.Boolean(nullable: false),
						IsDeleted = c.Boolean(nullable: false),
						Category_Id = c.Int(),
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Categories", t => t.Category_Id)
				.Index(t => t.Category_Id);

			Sql(DbMigrationExtensions.CreateViewQuery("MenuItemsView",
				"SELECT m.[Id] as MenuItemId, m.[CommonId] as MenuItemGuid, m.[Title], m.[Description], c.[Id] as CategoryId, c.[Title] as CategoryTitle From MenuItems m JOIN Categories c ON m.Category_Id = c.Id WHERE m.IsNewest=1 AND m.IsDeleted=0 AND c.IsDeleted=0"));

		}

		public override void Down()
		{
			Sql(DbMigrationExtensions.DropViewQuery("MenuItemsView"));

			DropForeignKey("dbo.MenuItems", "Category_Id", "dbo.Categories");
			DropIndex("dbo.MenuItems", new[] { "Category_Id" });
			DropTable("dbo.MenuItems");
		}
	}
}
