namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleFileUpload : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Article", "Journal_Id", "dbo.Journal");
            DropIndex("dbo.Article", new[] { "Journal_Id" });
            RenameColumn(table: "dbo.Article", name: "Journal_Id", newName: "JournalId");
            AddColumn("dbo.Article", "FilePath", c => c.String());
            AlterColumn("dbo.Article", "JournalId", c => c.Long());
            CreateIndex("dbo.Article", "JournalId");
            AddForeignKey("dbo.Article", "JournalId", "dbo.Journal", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Article", "JournalId", "dbo.Journal");
            DropIndex("dbo.Article", new[] { "JournalId" });
            AlterColumn("dbo.Article", "JournalId", c => c.Long());
            DropColumn("dbo.Article", "FilePath");
            RenameColumn(table: "dbo.Article", name: "JournalId", newName: "Journal_Id");
            CreateIndex("dbo.Article", "Journal_Id");
            AddForeignKey("dbo.Article", "Journal_Id", "dbo.Journal", "Id");
        }
    }
}
