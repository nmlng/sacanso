namespace TaskasWorkFlowApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parameterDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parameters", "ParameterDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Parameters", "ParameterDescription");
        }
    }
}
