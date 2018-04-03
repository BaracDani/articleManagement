namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ActivityLog", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ActivityLog", "UserId", c => c.Long(nullable: false));
        }
    }
}
