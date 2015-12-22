namespace WaiterManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedWaitertoOrderandOrdersView : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Waiter_Id", c => c.Int());
            CreateIndex("dbo.Orders", "Waiter_Id");
            AddForeignKey("dbo.Orders", "Waiter_Id", "dbo.Waiters", "Id");

			Sql(DbMigrationExtensions.CreateViewQuery("OrdersView",@"
SELECT o.Id AS OrderId, o.Created, o.Status, o.Comment, SUM(miq.Quantity) AS Quantity, 
mi.Title AS MenuItem, t.Id as TableId, t.Title as TableTitle, w.Id as WaiterId, u.Login AS WaiterLogin
FROM Orders o JOIN MenuItemsQuantities miq ON o.Id = miq.Order_Id
JOIN MenuItems mi ON miq.Item_Id = mi.Id  
JOIN Tables t ON o.Id = t.Id
LEFT JOIN Waiters w ON o.Id = w.Id
LEFT JOIN Users u ON w.User_Id = u.Id
GROUP BY o.Id, o.Id, o.Created, o.Status, o.Comment, mi.Title, t.Id, t.Title, w.Id, u.Login"));
        }
        
        public override void Down()
        {
			Sql(DbMigrationExtensions.DropViewQuery("OrdersView"));

            DropForeignKey("dbo.Orders", "Waiter_Id", "dbo.Waiters");
            DropIndex("dbo.Orders", new[] { "Waiter_Id" });
            DropColumn("dbo.Orders", "Waiter_Id");
        }
    }
}
