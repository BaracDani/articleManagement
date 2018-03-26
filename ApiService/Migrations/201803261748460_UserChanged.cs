namespace ApiService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FirstName", c => c.String(maxLength: 100));
            AddColumn("dbo.Users", "LastName", c => c.String(maxLength: 100));
            AddColumn("dbo.Users", "JoinDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "JoinDate");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "FirstName");
        }
    }
}
