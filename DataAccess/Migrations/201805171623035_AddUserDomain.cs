namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserDomain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserDomain",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        DomainId = c.Long(nullable: false),
                        Active = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.DomainId })
                .ForeignKey("dbo.Domain", t => t.DomainId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.DomainId)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDomain", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserDomain", "DomainId", "dbo.Domain");
            DropIndex("dbo.UserDomain", new[] { "User_Id" });
            DropIndex("dbo.UserDomain", new[] { "DomainId" });
            DropTable("dbo.UserDomain");
        }
    }
}
