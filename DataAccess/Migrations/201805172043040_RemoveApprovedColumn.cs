namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveApprovedColumn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ReviewedArticle", "Approved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReviewedArticle", "Approved", c => c.Boolean(nullable: false));
        }
    }
}
