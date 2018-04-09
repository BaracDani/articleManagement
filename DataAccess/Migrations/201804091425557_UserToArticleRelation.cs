namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserToArticleRelation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Article", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Article", "UserId");
            AddForeignKey("dbo.Article", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Article", "UserId", "dbo.Users");
            DropIndex("dbo.Article", new[] { "UserId" });
            AlterColumn("dbo.Article", "UserId", c => c.String());
        }
    }
}
