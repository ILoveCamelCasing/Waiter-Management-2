namespace WaiterManagement.DAL.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class LoginableWaiterAndTable : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.Tables", "Login", c => c.String());
      AddColumn("dbo.Waiters", "Login", c => c.String());
    }

    public override void Down()
    {
      DropColumn("dbo.Waiters", "Login");
      DropColumn("dbo.Tables", "Login");

      
    }
  }
}
