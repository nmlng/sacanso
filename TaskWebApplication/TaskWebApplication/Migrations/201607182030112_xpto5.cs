namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xpto5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JobParameters", "TaskaId", "dbo.Taskas");
            DropIndex("dbo.JobParameters", new[] { "TaskaId" });
            DropColumn("dbo.JobParameters", "TaskaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobParameters", "TaskaId", c => c.Int(nullable: false));
            CreateIndex("dbo.JobParameters", "TaskaId");
            AddForeignKey("dbo.JobParameters", "TaskaId", "dbo.Taskas", "Id", cascadeDelete: true);
        }
    }
}
