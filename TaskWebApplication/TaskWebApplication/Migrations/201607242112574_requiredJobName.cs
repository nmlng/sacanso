namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredJobName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "JobName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "JobName", c => c.String());
        }
    }
}
