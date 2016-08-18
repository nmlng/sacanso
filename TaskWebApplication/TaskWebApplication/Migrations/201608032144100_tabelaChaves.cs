namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tabelaChaves : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParentTaskas",
                c => new
                    {
                        TaskaId = c.Int(nullable: false),
                        ParentTaskaId = c.Int(nullable: false),
                        SubTaska_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.TaskaId, t.ParentTaskaId })
                .ForeignKey("dbo.SubTaskas", t => t.TaskaId, cascadeDelete: false)
                .ForeignKey("dbo.SubTaskas", t => t.ParentTaskaId, cascadeDelete: false)
                .ForeignKey("dbo.SubTaskas", t => t.SubTaska_Id)
                .Index(t => t.TaskaId)
                .Index(t => t.ParentTaskaId)
                .Index(t => t.SubTaska_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParentTaskas", "SubTaska_Id", "dbo.SubTaskas");
            DropForeignKey("dbo.ParentTaskas", "ParentTaskaId", "dbo.SubTaskas");
            DropForeignKey("dbo.ParentTaskas", "TaskaId", "dbo.SubTaskas");
            DropIndex("dbo.ParentTaskas", new[] { "SubTaska_Id" });
            DropIndex("dbo.ParentTaskas", new[] { "ParentTaskaId" });
            DropIndex("dbo.ParentTaskas", new[] { "TaskaId" });
            DropTable("dbo.ParentTaskas");
        }
    }
}
