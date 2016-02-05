namespace WaiterManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUnlockCodetoReservationOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservationOrders", "UnlockCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReservationOrders", "UnlockCode");
        }
    }
}
