namespace WaiterManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTabletoReservationOrder : DbMigration
    {
		public override void Up()
		{
			AddColumn("dbo.ReservationOrders", "Table_Id", c => c.Int());
			CreateIndex("dbo.ReservationOrders", "Table_Id");
			AddForeignKey("dbo.ReservationOrders", "Table_Id", "dbo.Tables", "Id");

			Sql(DbMigrationExtensions.CreateViewQuery("ReservationsView",
@"SELECT r.Id AS ReservationId, r.UnlockCode, u.Login AS ClientLogin, r.Created, r.ReservationTime, r.Status, r.Comment, rmiq.Quantity, mi.Title AS MenuItem, r.Table_Id AS TableId, t.Title AS TableTitle
FROM ReservationOrders r
LEFT JOIN WebClients wc ON wc.Id = r.Client_Id
LEFT JOIN Users u ON u.Id = wc.User_Id
LEFT JOIN ReservationMenuItemQuantities rmiq ON rmiq.ReservationOrder_Id=r.Id
LEFT JOIN MenuItems mi ON mi.Id = rmiq.Item_Id
LEFT JOIN Tables t ON t.Id = r.Table_Id"));
		}

		public override void Down()
		{
			Sql(DbMigrationExtensions.DropViewQuery("ReservationsView"));

			DropForeignKey("dbo.ReservationOrders", "Table_Id", "dbo.Tables");
			DropIndex("dbo.ReservationOrders", new[] { "Table_Id" });
			DropColumn("dbo.ReservationOrders", "Table_Id");
		}
	}
}
