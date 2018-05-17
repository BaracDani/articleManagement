namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserDomain2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserDomain", "User_Id", "dbo.Users");
            DropIndex("dbo.UserDomain", new[] { "User_Id" });
            DropColumn("dbo.UserDomain", "User_Id");
            DropPrimaryKey("dbo.UserDomain");
            AlterColumn("dbo.UserDomain", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserDomain", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.UserDomain", new[] { "UserId", "DomainId" });
            CreateIndex("dbo.UserDomain", "UserId");
            AddForeignKey("dbo.UserDomain", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDomain", "UserId", "dbo.Users");
            DropIndex("dbo.UserDomain", new[] { "UserId" });
            DropPrimaryKey("dbo.UserDomain");
            AlterColumn("dbo.UserDomain", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.UserDomain", "UserId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.UserDomain", new[] { "UserId", "DomainId" });
            RenameColumn(table: "dbo.UserDomain", name: "UserId", newName: "User_Id");
            AddColumn("dbo.UserDomain", "UserId", c => c.Long(nullable: false));
            CreateIndex("dbo.UserDomain", "User_Id");
            AddForeignKey("dbo.UserDomain", "User_Id", "dbo.Users", "UserId");
        }
    }
}
