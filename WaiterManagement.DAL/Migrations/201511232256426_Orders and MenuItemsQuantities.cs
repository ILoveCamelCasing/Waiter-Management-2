namespace WaiterManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdersandMenuItemsQuantities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuItemsQuantities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Item_Id = c.Int(),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItems", t => t.Item_Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Item_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuItemsQuantities", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.MenuItemsQuantities", "Item_Id", "dbo.MenuItems");
            DropIndex("dbo.MenuItemsQuantities", new[] { "Order_Id" });
            DropIndex("dbo.MenuItemsQuantities", new[] { "Item_Id" });
            DropTable("dbo.Orders");
            DropTable("dbo.MenuItemsQuantities");
        }
    }
}
