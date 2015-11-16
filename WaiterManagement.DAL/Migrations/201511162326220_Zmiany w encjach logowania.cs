namespace WaiterManagement.DAL.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class Zmianywencjachlogowania : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.Tables", "User_Id", c => c.Int());
			AddColumn("dbo.Users", "Login", c => c.String());
			AddColumn("dbo.Waiters", "User_Id", c => c.Int());
			CreateIndex("dbo.Tables", "User_Id");
			CreateIndex("dbo.Waiters", "User_Id");
			AddForeignKey("dbo.Tables", "User_Id", "dbo.Users", "Id");
			AddForeignKey("dbo.Waiters", "User_Id", "dbo.Users", "Id");
			DropColumn("dbo.Tables", "Login");
			DropColumn("dbo.Users", "UserId");
			DropColumn("dbo.Waiters", "Login");


			Sql(DbMigrationExtensions.DropViewQuery("dbo.UsersView"));
			Sql(DbMigrationExtensions.AlterViewQuery("dbo.TablesView",
				@"SELECT t.[Id] AS [TableId], t.[CommonId] AS [TableGuid], [Title], [Description], u.[Login], u.[SecondHash], u.[CommonId] AS [UserId] 
				  FROM [dbo].[Tables] t join [dbo].[Users] u on t.[User_Id] = u.[Id] WHERE t.[IsNewest]=1 AND t.[IsDeleted]=0"));
			Sql(DbMigrationExtensions.AlterViewQuery("dbo.WaitersView",
				"SELECT w.[Id] AS [WaiterId], w.[CommonId] AS [WaiterGuid], [FirstName], [LastName], u.[Login], u.[SecondHash], u.[CommonId] AS [UserId] FROM [dbo].[Waiters] w join [dbo].[Users] u on w.User_Id = u.Id WHERE w.[IsNewest]=1 AND w.[IsDeleted]=0"));
		}

		public override void Down()
		{
			AddColumn("dbo.Waiters", "Login", c => c.String());
			AddColumn("dbo.Users", "UserId", c => c.Guid(nullable: false));
			AddColumn("dbo.Tables", "Login", c => c.String());
			DropForeignKey("dbo.Waiters", "User_Id", "dbo.Users");
			DropForeignKey("dbo.Tables", "User_Id", "dbo.Users");
			DropIndex("dbo.Waiters", new[] { "User_Id" });
			DropIndex("dbo.Tables", new[] { "User_Id" });
			DropColumn("dbo.Waiters", "User_Id");
			DropColumn("dbo.Users", "Login");
			DropColumn("dbo.Tables", "User_Id");

			Sql(DbMigrationExtensions.CreateViewQuery("UsersView",
				"SELECT [Id], [CommonId], [UserId], [SecondHash] From Users WHERE IsNewest=1 AND IsDeleted=0"));
			Sql(DbMigrationExtensions.AlterViewQuery("TablesView",
		"SELECT [Id] as TableId, [CommonId] as TableGuid, [Title], [Description], [Login] From Tables WHERE IsNewest=1 AND IsDeleted=0"));
			Sql(DbMigrationExtensions.AlterViewQuery("WaitersView",
			  "SELECT [Id] as WaiterId, [CommonId] as WaiterGuid, [FirstName], [LastName], [Login] From Waiters WHERE IsNewest=1 AND IsDeleted=0"));
		}
	}
}
