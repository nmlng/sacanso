namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xpto4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "Parameter_Id", "dbo.Parameters");
            DropIndex("dbo.Jobs", new[] { "Parameter_Id" });
            CreateTable(
                "dbo.JobParameters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskaId = c.Int(nullable: false),
                        ParameterId = c.Int(nullable: false),
                        ParameterValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Parameters", t => t.ParameterId, cascadeDelete: true)
                .ForeignKey("dbo.Taskas", t => t.TaskaId, cascadeDelete: false)
                .Index(t => t.TaskaId)
                .Index(t => t.ParameterId);
            
            DropColumn("dbo.Jobs", "Parameter_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "Parameter_Id", c => c.Int());
            DropForeignKey("dbo.JobParameters", "TaskaId", "dbo.Taskas");
            DropForeignKey("dbo.JobParameters", "ParameterId", "dbo.Parameters");
            DropIndex("dbo.JobParameters", new[] { "ParameterId" });
            DropIndex("dbo.JobParameters", new[] { "TaskaId" });
            DropTable("dbo.JobParameters");
            CreateIndex("dbo.Jobs", "Parameter_Id");
            AddForeignKey("dbo.Jobs", "Parameter_Id", "dbo.Parameters", "Id");
        }
    }
}
