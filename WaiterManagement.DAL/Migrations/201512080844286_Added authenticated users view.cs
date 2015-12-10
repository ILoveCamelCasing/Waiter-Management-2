using WaiterManagement.Common.Views;

namespace WaiterManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedauthenticatedusersview : DbMigration
    {
		public override void Up()
		{
			Sql(DbMigrationExtensions.CreateViewQuery("AuthenticatedUsersView", string.Format(
				@"SELECT u.Id AS [UserId], u.Login AS [Login], {0} AS [Type], a.UserToken AS [Token] 
				  FROM dbo.Tables t 
				  JOIN Users u ON t.User_Id = u.Id
				  JOIN activeUsers a ON a.UserId = u.CommonId 
				  WHERE u.IsNewest=1 AND u.IsDeleted=0 AND a.TokenCreation >= DateAdd(DAY,-1,GETDATE())
				  UNION
				  SELECT u.Id AS [UserId], u.Login AS [Login], {1} AS [Type], a.UserToken AS [Token] 
				  FROM dbo.Waiters w 
				  JOIN Users u ON w.User_Id = u.Id
				  JOIN activeUsers a ON a.UserId = u.CommonId 
				  WHERE u.IsNewest=1 AND u.IsDeleted=0 AND a.TokenCreation >= DateAdd(DAY,-1,GETDATE())"
				, (int)UserType.Table, (int)UserType.Waiter)));

		}

		public override void Down()
		{
			Sql(DbMigrationExtensions.DropViewQuery("AuthenticatedUsersView"));
		}
    }
}
