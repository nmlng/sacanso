namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parameters4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Jobs", "JobName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "JobName", c => c.String());
        }
    }
}
