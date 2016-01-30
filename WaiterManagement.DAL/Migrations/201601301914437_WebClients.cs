namespace WaiterManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebClients : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WebClients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Mail = c.String(),
                        Phone = c.String(),
                        CommonId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        Version = c.Int(nullable: false),
                        IsNewest = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WebClients", "User_Id", "dbo.Users");
            DropIndex("dbo.WebClients", new[] { "User_Id" });
            DropTable("dbo.WebClients");
        }
    }
}
