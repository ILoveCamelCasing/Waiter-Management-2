namespace WaiterManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReservationTimetoReservationOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservationOrders", "ReservationTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReservationOrders", "ReservationTime");
        }
    }
}
