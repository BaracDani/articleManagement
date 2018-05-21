namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReviewStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReviewedArticle", "ReviewStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReviewedArticle", "ReviewStatus");
        }
    }
}
