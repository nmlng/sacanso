namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parameters : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "TaskaId", "dbo.Taskas");
            DropForeignKey("dbo.Jobs", "TaskaId", "dbo.Jobs");
            AddColumn("dbo.Jobs", "ParameterId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "ParameterId");
            AddForeignKey("dbo.Jobs", "ParameterId", "dbo.Parameters", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Jobs", "TaskaId", "dbo.Taskas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "TaskaId", "dbo.Taskas");
            DropForeignKey("dbo.Jobs", "ParameterId", "dbo.Parameters");
            DropIndex("dbo.Jobs", new[] { "ParameterId" });
            DropColumn("dbo.Jobs", "ParameterId");
            AddForeignKey("dbo.Jobs", "TaskaId", "dbo.Jobs", "Id");
            AddForeignKey("dbo.Jobs", "TaskaId", "dbo.Taskas", "Id", cascadeDelete: true);
        }
    }
}
