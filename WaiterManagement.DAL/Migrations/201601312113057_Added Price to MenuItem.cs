namespace WaiterManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPricetoMenuItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuItems", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            Sql(DbMigrationExtensions.AlterViewQuery("MenuItemsView",
@"SELECT m.Id AS MenuItemId, m.CommonId AS MenuItemGuid, m.Title, m.Description, c.Id AS CategoryId, c.Title AS CategoryTitle, m.Price
FROM     dbo.MenuItems AS m INNER JOIN
                  dbo.Categories AS c ON m.Category_Id = c.Id
WHERE  (m.IsNewest = 1) AND (m.IsDeleted = 0) AND (c.IsDeleted = 0)"));
        }
        
        public override void Down()
        {
			Sql(DbMigrationExtensions.AlterViewQuery("MenuItemsView",
@"SELECT m.Id AS MenuItemId, m.CommonId AS MenuItemGuid, m.Title, m.Description, c.Id AS CategoryId, c.Title AS CategoryTitle
FROM     dbo.MenuItems AS m INNER JOIN
                  dbo.Categories AS c ON m.Category_Id = c.Id
WHERE  (m.IsNewest = 1) AND (m.IsDeleted = 0) AND (c.IsDeleted = 0)"));
			DropColumn("dbo.MenuItems", "Price");
        }
    }
}
