namespace TaskasWorkFlowApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskaRun : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskaRuns", "ParentTaskaRunId", "dbo.TaskaRuns");
            DropForeignKey("dbo.TaskaRuns", "TaskaId", "dbo.Taskas");
            DropIndex("dbo.TaskaRuns", new[] { "ParentTaskaRunId" });
            DropIndex("dbo.TaskaRuns", "AK_Taska_TaskaRunName");
            RenameColumn(table: "dbo.TaskaRuns", name: "TaskaRunId", newName: "Id");
            CreateTable(
                "dbo.ParentChildTaskaRun",
                c => new
                    {
                        ChildTaskaRunId = c.Int(nullable: false),
                        ParentTaskaRunId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ChildTaskaRunId, t.ParentTaskaRunId })
                .ForeignKey("dbo.TaskaRuns", t => t.ChildTaskaRunId)
                .ForeignKey("dbo.TaskaRuns", t => t.ParentTaskaRunId)
                .Index(t => t.ChildTaskaRunId)
                .Index(t => t.ParentTaskaRunId);
            
            AddForeignKey("dbo.TaskaRuns", "TaskaId", "dbo.Taskas", "TaskaId", cascadeDelete: true);
            DropColumn("dbo.TaskaRuns", "ParentTaskaRunId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskaRuns", "ParentTaskaRunId", c => c.Int());
            DropForeignKey("dbo.TaskaRuns", "TaskaId", "dbo.Taskas");
            DropForeignKey("dbo.ParentChildTaskaRun", "ParentTaskaRunId", "dbo.TaskaRuns");
            DropForeignKey("dbo.ParentChildTaskaRun", "ChildTaskaRunId", "dbo.TaskaRuns");
            DropIndex("dbo.ParentChildTaskaRun", new[] { "ParentTaskaRunId" });
            DropIndex("dbo.ParentChildTaskaRun", new[] { "ChildTaskaRunId" });
            DropTable("dbo.ParentChildTaskaRun");
            RenameColumn(table: "dbo.TaskaRuns", name: "Id", newName: "TaskaRunId");
            CreateIndex("dbo.TaskaRuns", "TaskaRunName", unique: true, name: "AK_Taska_TaskaRunName");
            CreateIndex("dbo.TaskaRuns", "ParentTaskaRunId");
            AddForeignKey("dbo.TaskaRuns", "TaskaId", "dbo.Taskas", "TaskaId");
            AddForeignKey("dbo.TaskaRuns", "ParentTaskaRunId", "dbo.TaskaRuns", "TaskaRunId");
        }
    }
}
