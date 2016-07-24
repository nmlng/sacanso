namespace TaskWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parameters2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "ParameterId", "dbo.Parameters");
            DropIndex("dbo.Jobs", new[] { "ParameterId" });
            RenameColumn(table: "dbo.Jobs", name: "ParameterId", newName: "Parameter_Id");
            AddColumn("dbo.Jobs", "JobName", c => c.String());
            AddColumn("dbo.Jobs", "ParameterName", c => c.String());
            AddColumn("dbo.Jobs", "ParameterValue", c => c.String());
            AlterColumn("dbo.Jobs", "Parameter_Id", c => c.Int());
            CreateIndex("dbo.Jobs", "Parameter_Id");
            AddForeignKey("dbo.Jobs", "Parameter_Id", "dbo.Parameters", "Id");
            DropColumn("dbo.Jobs", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "Name", c => c.String());
            DropForeignKey("dbo.Jobs", "Parameter_Id", "dbo.Parameters");
            DropIndex("dbo.Jobs", new[] { "Parameter_Id" });
            AlterColumn("dbo.Jobs", "Parameter_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Jobs", "ParameterValue");
            DropColumn("dbo.Jobs", "ParameterName");
            DropColumn("dbo.Jobs", "JobName");
            RenameColumn(table: "dbo.Jobs", name: "Parameter_Id", newName: "ParameterId");
            CreateIndex("dbo.Jobs", "ParameterId");
            AddForeignKey("dbo.Jobs", "ParameterId", "dbo.Parameters", "Id", cascadeDelete: true);
        }
    }
}
