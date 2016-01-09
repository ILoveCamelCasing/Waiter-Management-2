namespace WaiterManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReadytoMenuItemsQuantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuItemsQuantities", "Ready", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuItemsQuantities", "Ready");
        }
    }
}
