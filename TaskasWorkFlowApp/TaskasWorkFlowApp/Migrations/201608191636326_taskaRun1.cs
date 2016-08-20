namespace TaskasWorkFlowApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskaRun1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TaskaRuns", name: "Id", newName: "TaskaRunId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.TaskaRuns", name: "TaskaRunId", newName: "Id");
        }
    }
}
