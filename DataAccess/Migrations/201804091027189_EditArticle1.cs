namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditArticle1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Article", "Rejected", c => c.Boolean(nullable: false));
            AddColumn("dbo.Article", "InPending", c => c.Boolean(nullable: false));
            AddColumn("dbo.Article", "InReview", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Article", "InReview");
            DropColumn("dbo.Article", "InPending");
            DropColumn("dbo.Article", "Rejected");
            DropColumn("dbo.Article", "Approved");
        }
    }
}
