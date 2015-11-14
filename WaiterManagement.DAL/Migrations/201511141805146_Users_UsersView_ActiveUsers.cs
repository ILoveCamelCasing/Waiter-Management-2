namespace WaiterManagement.DAL.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class Users_UsersView_ActiveUsers : DbMigration
	{
		public override void Up()
		{
			CreateTable(
					"dbo.ActiveUsers",
					c => new
					{
						Id = c.Int(nullable: false, identity: true),
						UserId = c.Guid(nullable: false),
						UserToken = c.Guid(nullable: false),
						TokenCreation = c.DateTime(nullable: false),
					})
					.PrimaryKey(t => t.Id);

			CreateTable(
					"dbo.Users",
					c => new
					{
						Id = c.Int(nullable: false, identity: true),
						UserId = c.Guid(nullable: false),
						SecondHash = c.String(),
						CommonId = c.Guid(nullable: false),
						Created = c.DateTime(nullable: false),
						Modified = c.DateTime(),
						Version = c.Int(nullable: false),
						IsNewest = c.Boolean(nullable: false),
						IsDeleted = c.Boolean(nullable: false),
					})
					.PrimaryKey(t => t.Id);

			Sql(DbMigrationExtensions.CreateViewQuery("UsersView",
				"SELECT [Id], [CommonId], [UserId], [SecondHash] From Users WHERE IsNewest=1 AND IsDeleted=0"));
		}

		public override void Down()
		{
			DropTable("dbo.UsersView");
			DropTable("dbo.Users");
			Sql(DbMigrationExtensions.DropViewQuery("UsersView"));
		}
	}
}
