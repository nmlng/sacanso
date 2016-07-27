namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ssss1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.JobResults", name: "Job", newName: "JobId");
            RenameIndex(table: "dbo.JobResults", name: "IX_Job", newName: "IX_JobId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.JobResults", name: "IX_JobId", newName: "IX_Job");
            RenameColumn(table: "dbo.JobResults", name: "JobId", newName: "Job");
        }
    }
}
