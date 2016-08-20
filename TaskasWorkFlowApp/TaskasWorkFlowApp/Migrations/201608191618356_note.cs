namespace TaskasWorkFlowApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class note : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "TaskaRunId", "dbo.TaskaRuns");
            DropIndex("dbo.Messages", new[] { "TaskaRunId" });
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        NoteText = c.String(),
                        TaskaId = c.Int(nullable: false),
                        TaskaRunId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NoteId)
                .ForeignKey("dbo.Taskas", t => t.TaskaId)
                .ForeignKey("dbo.TaskaRuns", t => t.TaskaRunId)
                .Index(t => t.TaskaId)
                .Index(t => t.TaskaRunId);
            
            DropTable("dbo.Messages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        MessageText = c.String(),
                        MessageType = c.Int(nullable: false),
                        TaskaRunId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId);
            
            DropForeignKey("dbo.Notes", "TaskaRunId", "dbo.TaskaRuns");
            DropForeignKey("dbo.Notes", "TaskaId", "dbo.Taskas");
            DropIndex("dbo.Notes", new[] { "TaskaRunId" });
            DropIndex("dbo.Notes", new[] { "TaskaId" });
            DropTable("dbo.Notes");
            CreateIndex("dbo.Messages", "TaskaRunId");
            AddForeignKey("dbo.Messages", "TaskaRunId", "dbo.TaskaRuns", "TaskaRunId");
        }
    }
}
