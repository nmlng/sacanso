namespace TaskasWorkFlowApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parameter_and_parameterRun : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParameterRuns",
                c => new
                    {
                        ParameterRunId = c.Int(nullable: false, identity: true),
                        ParameterValue = c.Int(nullable: false),
                        ParameterId = c.Int(nullable: false),
                        TaskaRunId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParameterRunId)
                .ForeignKey("dbo.Parameters", t => t.ParameterId)
                .ForeignKey("dbo.TaskaRuns", t => t.TaskaRunId)
                .Index(t => t.ParameterId)
                .Index(t => t.TaskaRunId);
            
            CreateTable(
                "dbo.Parameters",
                c => new
                    {
                        ParameterId = c.Int(nullable: false, identity: true),
                        ParameterName = c.String(),
                        ParameterType = c.Int(nullable: false),
                        TaskaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParameterId)
                .ForeignKey("dbo.Taskas", t => t.TaskaId)
                .Index(t => t.TaskaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParameterRuns", "TaskaRunId", "dbo.TaskaRuns");
            DropForeignKey("dbo.ParameterRuns", "ParameterId", "dbo.Parameters");
            DropForeignKey("dbo.Parameters", "TaskaId", "dbo.Taskas");
            DropIndex("dbo.Parameters", new[] { "TaskaId" });
            DropIndex("dbo.ParameterRuns", new[] { "TaskaRunId" });
            DropIndex("dbo.ParameterRuns", new[] { "ParameterId" });
            DropTable("dbo.Parameters");
            DropTable("dbo.ParameterRuns");
        }
    }
}
