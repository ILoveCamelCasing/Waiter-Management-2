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

      Sql(DbMigrationExtensions.DropViewQuery("TablesView"));
      Sql(DbMigrationExtensions.DropViewQuery("WaitersView"));

      Sql(DbMigrationExtensions.CreateViewQuery("TablesView",
        "SELECT [Id] as TableId, [CommonId] as TableGuid, [Title], [Description], [Login] From Tables WHERE IsNewest=1 AND IsDeleted=0"));
      Sql(DbMigrationExtensions.CreateViewQuery("WaitersView",
        "SELECT [Id] as WaiterId, [CommonId] as WaiterGuid, [FirstName], [LastName], [Login] From Waiters WHERE IsNewest=1 AND IsDeleted=0"));
    }

    public override void Down()
    {
      DropColumn("dbo.Waiters", "Login");
      DropColumn("dbo.Tables", "Login");

      Sql(DbMigrationExtensions.DropViewQuery("TablesView"));
      Sql(DbMigrationExtensions.DropViewQuery("WaitersView"));
    }
  }
}
