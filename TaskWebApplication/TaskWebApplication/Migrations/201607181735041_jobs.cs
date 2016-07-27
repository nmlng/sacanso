namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TaskaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.TaskaId)
                .ForeignKey("dbo.Taskas", t => t.TaskaId, cascadeDelete: true)
                .Index(t => t.TaskaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "TaskaId", "dbo.Taskas");
            DropForeignKey("dbo.Jobs", "TaskaId", "dbo.Jobs");
            DropIndex("dbo.Jobs", new[] { "TaskaId" });
            DropTable("dbo.Jobs");
        }
    }
}
