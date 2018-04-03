namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdAddedArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Article", "UserId");
        }
    }
}
