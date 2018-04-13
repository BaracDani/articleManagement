namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditArticleAddApprovalStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "ApprovalStatus", c => c.Int(nullable: false));
            DropColumn("dbo.Article", "Approved");
            DropColumn("dbo.Article", "Rejected");
            DropColumn("dbo.Article", "InPending");
            DropColumn("dbo.Article", "InReview");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Article", "InReview", c => c.Boolean(nullable: false));
            AddColumn("dbo.Article", "InPending", c => c.Boolean(nullable: false));
            AddColumn("dbo.Article", "Rejected", c => c.Boolean(nullable: false));
            AddColumn("dbo.Article", "Approved", c => c.Boolean(nullable: false));
            DropColumn("dbo.Article", "ApprovalStatus");
        }
    }
}
