namespace WaiterManagement.DAL.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class LoginableWaiterViewAndTableView : DbMigration
  {
    public override void Up()
    {
      Sql(DbMigrationExtensions.DropViewQuery("TablesView"));
      Sql(DbMigrationExtensions.DropViewQuery("WaitersView"));

      Sql(DbMigrationExtensions.CreateViewQuery("TablesView",
        "SELECT [Id] as TableId, [CommonId] as TableGuid, [Title], [Description], [Login] From Tables WHERE IsNewest=1 AND IsDeleted=0"));
      Sql(DbMigrationExtensions.CreateViewQuery("WaitersView",
        "SELECT [Id] as WaiterId, [CommonId] as WaiterGuid, [FirstName], [LastName], [Login] From Waiters WHERE IsNewest=1 AND IsDeleted=0"));
    }

    public override void Down()
    {
      Sql(DbMigrationExtensions.DropViewQuery("TablesView"));
      Sql(DbMigrationExtensions.DropViewQuery("WaitersView"));
    }
  }
}
