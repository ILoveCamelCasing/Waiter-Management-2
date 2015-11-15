namespace WaiterManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablesUnicateTitle : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tables", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tables", "Title", c => c.String());
        }
    }
}
