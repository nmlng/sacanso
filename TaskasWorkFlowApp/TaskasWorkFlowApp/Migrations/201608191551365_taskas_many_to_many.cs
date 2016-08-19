namespace TaskasWorkFlowApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskas_many_to_many : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Taskas", "ParentTaskaId", "dbo.Taskas");
            DropIndex("dbo.Taskas", new[] { "ParentTaskaId" });
            DropIndex("dbo.Taskas", "AK_Taska_TaskaName");
            CreateTable(
                "dbo.ParentChildTaska",
                c => new
                    {
                        ChildTaskaId = c.Int(nullable: false),
                        ParentTaskaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ChildTaskaId, t.ParentTaskaId })
                .ForeignKey("dbo.Taskas", t => t.ChildTaskaId)
                .ForeignKey("dbo.Taskas", t => t.ParentTaskaId)
                .Index(t => t.ChildTaskaId)
                .Index(t => t.ParentTaskaId);
            
            AddColumn("dbo.Taskas", "Description", c => c.String());
            AlterColumn("dbo.Taskas", "TaskaName", c => c.String());
            DropColumn("dbo.Taskas", "ParentTaskaId");
            DropColumn("dbo.Taskas", "TaskaDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Taskas", "TaskaDescription", c => c.String());
            AddColumn("dbo.Taskas", "ParentTaskaId", c => c.Int());
            DropForeignKey("dbo.ParentChildTaska", "ParentTaskaId", "dbo.Taskas");
            DropForeignKey("dbo.ParentChildTaska", "ChildTaskaId", "dbo.Taskas");
            DropIndex("dbo.ParentChildTaska", new[] { "ParentTaskaId" });
            DropIndex("dbo.ParentChildTaska", new[] { "ChildTaskaId" });
            AlterColumn("dbo.Taskas", "TaskaName", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Taskas", "Description");
            DropTable("dbo.ParentChildTaska");
            CreateIndex("dbo.Taskas", "TaskaName", unique: true, name: "AK_Taska_TaskaName");
            CreateIndex("dbo.Taskas", "ParentTaskaId");
            AddForeignKey("dbo.Taskas", "ParentTaskaId", "dbo.Taskas", "TaskaId");
        }
    }
}
