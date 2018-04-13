namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditArticle2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "Title", c => c.String());
            AddColumn("dbo.Article", "Author", c => c.String());
            AddColumn("dbo.Article", "Deadline", c => c.String());
            DropColumn("dbo.Article", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Article", "Name", c => c.String());
            DropColumn("dbo.Article", "Deadline");
            DropColumn("dbo.Article", "Author");
            DropColumn("dbo.Article", "Title");
        }
    }
}
