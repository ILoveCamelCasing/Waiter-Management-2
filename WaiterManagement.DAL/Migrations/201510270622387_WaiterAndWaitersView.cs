namespace WaiterManagement.DAL.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class WaiterAndWaitersView : DbMigration
  {
    public override void Up()
    {
      CreateTable(
          "dbo.Waiters",
          c => new
          {
            Id = c.Int(nullable: false, identity: true),
            FirstName = c.String(),
            LastName = c.String(),
            CommonId = c.Guid(nullable: false),
            Created = c.DateTime(nullable: false),
            Modified = c.DateTime(),
            Version = c.Int(nullable: false),
            IsNewest = c.Boolean(nullable: false),
            IsDeleted = c.Boolean(nullable: false),
          })
          .PrimaryKey(t => t.Id);

      Sql(DbMigrationExtensions.CreateViewQuery("WaitersView",
        "SELECT [Id] as WaiterId, [CommonId] as WaiterGuid, [FirstName], [LastName] From Waiters WHERE IsNewest=1 AND IsDeleted=0"));
    }

    public override void Down()
    {
      DropTable("dbo.Waiters");
      Sql(DbMigrationExtensions.DropViewQuery("WaitersView"));
    }
  }
}
