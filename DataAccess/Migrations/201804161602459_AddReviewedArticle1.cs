namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReviewedArticle1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReviewedArticle",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ArticleId = c.Long(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        Id = c.Long(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ArticleId })
                .ForeignKey("dbo.Article", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ArticleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReviewedArticle", "UserId", "dbo.Users");
            DropForeignKey("dbo.ReviewedArticle", "ArticleId", "dbo.Article");
            DropIndex("dbo.ReviewedArticle", new[] { "ArticleId" });
            DropIndex("dbo.ReviewedArticle", new[] { "UserId" });
            DropTable("dbo.ReviewedArticle");
        }
    }
}
