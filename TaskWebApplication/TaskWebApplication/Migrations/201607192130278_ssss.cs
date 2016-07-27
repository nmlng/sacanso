namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ssss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Job = c.Int(nullable: false),
                        Description = c.String(),
                        Command = c.String(),
                        ErrorMessage = c.String(),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.Job, cascadeDelete: true)
                .Index(t => t.Job);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobResults", "Job", "dbo.Jobs");
            DropIndex("dbo.JobResults", new[] { "Job" });
            DropTable("dbo.JobResults");
        }
    }
}
