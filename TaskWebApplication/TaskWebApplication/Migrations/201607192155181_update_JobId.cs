namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_JobId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.JobParameters", name: "Job", newName: "JobId");
            RenameIndex(table: "dbo.JobParameters", name: "IX_Job", newName: "IX_JobId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.JobParameters", name: "IX_JobId", newName: "IX_Job");
            RenameColumn(table: "dbo.JobParameters", name: "JobId", newName: "Job");
        }
    }
}
