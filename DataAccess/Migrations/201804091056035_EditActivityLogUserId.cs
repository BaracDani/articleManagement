namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditActivityLogUserId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ActivityLog", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActivityLog", "UserId", c => c.String());
        }
    }
}
