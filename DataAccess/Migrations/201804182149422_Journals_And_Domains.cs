namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Journals_And_Domains : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Domain",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Journal",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        IsPublished = c.Boolean(nullable: false),
                        PublishDate = c.String(),
                        EditorId = c.String(nullable: false, maxLength: 128),
                        DomainId = c.Long(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domain", t => t.DomainId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.EditorId, cascadeDelete: true)
                .Index(t => t.EditorId)
                .Index(t => t.DomainId);
            
            AddColumn("dbo.Article", "Journal_Id", c => c.Long());
            CreateIndex("dbo.Article", "Journal_Id");
            AddForeignKey("dbo.Article", "Journal_Id", "dbo.Journal", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Journal", "EditorId", "dbo.Users");
            DropForeignKey("dbo.Journal", "DomainId", "dbo.Domain");
            DropForeignKey("dbo.Article", "Journal_Id", "dbo.Journal");
            DropIndex("dbo.Journal", new[] { "DomainId" });
            DropIndex("dbo.Journal", new[] { "EditorId" });
            DropIndex("dbo.Article", new[] { "Journal_Id" });
            DropColumn("dbo.Article", "Journal_Id");
            DropTable("dbo.Journal");
            DropTable("dbo.Domain");
        }
    }
}
