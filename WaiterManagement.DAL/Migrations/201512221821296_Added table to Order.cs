namespace WaiterManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedtabletoOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Table_Id", c => c.Int());
            CreateIndex("dbo.Orders", "Table_Id");
            AddForeignKey("dbo.Orders", "Table_Id", "dbo.Tables", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Table_Id", "dbo.Tables");
            DropIndex("dbo.Orders", new[] { "Table_Id" });
            DropColumn("dbo.Orders", "Table_Id");
        }
    }
}
