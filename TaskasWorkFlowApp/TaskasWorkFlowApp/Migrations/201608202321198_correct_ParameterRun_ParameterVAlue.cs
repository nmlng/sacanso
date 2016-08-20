namespace TaskasWorkFlowApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correct_ParameterRun_ParameterVAlue : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ParameterRuns", "ParameterValue", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ParameterRuns", "ParameterValue", c => c.Int(nullable: false));
        }
    }
}
