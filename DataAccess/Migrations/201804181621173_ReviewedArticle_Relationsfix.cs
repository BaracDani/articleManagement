namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewedArticle_Relationsfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReviewedArticle", "ArticleId", "dbo.Article");
            DropForeignKey("dbo.ReviewedArticle", "UserId", "dbo.Users");
            AddForeignKey("dbo.ReviewedArticle", "ArticleId", "dbo.Article", "Id");
            AddForeignKey("dbo.ReviewedArticle", "UserId", "dbo.Users", "UserId");
            DropColumn("dbo.ReviewedArticle", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReviewedArticle", "Id", c => c.Long(nullable: false));
            DropForeignKey("dbo.ReviewedArticle", "UserId", "dbo.Users");
            DropForeignKey("dbo.ReviewedArticle", "ArticleId", "dbo.Article");
            AddForeignKey("dbo.ReviewedArticle", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.ReviewedArticle", "ArticleId", "dbo.Article", "Id");
        }
    }
}
