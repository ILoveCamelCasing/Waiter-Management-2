namespace WaiterManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebClientsView : DbMigration
    {
        public override void Up()
        {
			Sql(DbMigrationExtensions.CreateViewQuery("WebClientsView",
@"SELECT wc.[Id] as WebClientId, wc.[CommonId] as WebClientGuid, [FirstName], [LastName], [Phone], [Mail],[Login], [SecondHash], u.[CommonId] as UserId
From WebClients wc
INNER JOIN Users u ON wc.User_Id = u.Id
WHERE wc.IsNewest = 1 AND wc.IsDeleted = 0"));
		}
        
        public override void Down()
        {
            Sql(DbMigrationExtensions.DropViewQuery("WebClientsView"));
        }
    }
}
