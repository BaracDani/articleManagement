namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReviewPoints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReviewedArticle", "ReviewPoints", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReviewedArticle", "ReviewPoints");
        }
    }
}
