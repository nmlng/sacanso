namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xpto7 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.JobParameters", name: "ParameterId", newName: "Parameter");
            RenameColumn(table: "dbo.JobParameters", name: "JobId", newName: "Job");
            RenameIndex(table: "dbo.JobParameters", name: "IX_JobId", newName: "IX_Job");
            RenameIndex(table: "dbo.JobParameters", name: "IX_ParameterId", newName: "IX_Parameter");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.JobParameters", name: "IX_Parameter", newName: "IX_ParameterId");
            RenameIndex(table: "dbo.JobParameters", name: "IX_Job", newName: "IX_JobId");
            RenameColumn(table: "dbo.JobParameters", name: "Job", newName: "JobId");
            RenameColumn(table: "dbo.JobParameters", name: "Parameter", newName: "ParameterId");
        }
    }
}
