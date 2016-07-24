namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Status", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Status");
        }
    }
}
