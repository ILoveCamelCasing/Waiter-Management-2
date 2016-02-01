namespace WaiterManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservationOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReservationMenuItemQuantities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Item_Id = c.Int(),
                        ReservationOrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItems", t => t.Item_Id)
                .ForeignKey("dbo.ReservationOrders", t => t.ReservationOrder_Id)
                .Index(t => t.Item_Id)
                .Index(t => t.ReservationOrder_Id);
            
            CreateTable(
                "dbo.ReservationOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Comment = c.String(),
                        Client_Id = c.Int(),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebClients", t => t.Client_Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.Order_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationMenuItemQuantities", "ReservationOrder_Id", "dbo.ReservationOrders");
            DropForeignKey("dbo.ReservationOrders", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.ReservationOrders", "Client_Id", "dbo.WebClients");
            DropForeignKey("dbo.ReservationMenuItemQuantities", "Item_Id", "dbo.MenuItems");
            DropIndex("dbo.ReservationOrders", new[] { "Order_Id" });
            DropIndex("dbo.ReservationOrders", new[] { "Client_Id" });
            DropIndex("dbo.ReservationMenuItemQuantities", new[] { "ReservationOrder_Id" });
            DropIndex("dbo.ReservationMenuItemQuantities", new[] { "Item_Id" });
            DropTable("dbo.ReservationOrders");
            DropTable("dbo.ReservationMenuItemQuantities");
        }
    }
}
