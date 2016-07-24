namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xpto6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobParameters", "JobId", c => c.Int(nullable: false));
            CreateIndex("dbo.JobParameters", "JobId");
            AddForeignKey("dbo.JobParameters", "JobId", "dbo.Jobs", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobParameters", "JobId", "dbo.Jobs");
            DropIndex("dbo.JobParameters", new[] { "JobId" });
            DropColumn("dbo.JobParameters", "JobId");
        }
    }
}
