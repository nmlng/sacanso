namespace TaskasWorkFlowApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        MessageText = c.String(),
                        MessageType = c.Int(nullable: false),
                        TaskaRunId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.TaskaRuns", t => t.TaskaRunId)
                .Index(t => t.TaskaRunId);
            
            CreateTable(
                "dbo.TaskaRuns",
                c => new
                    {
                        TaskaRunId = c.Int(nullable: false, identity: true),
                        ParentTaskaRunId = c.Int(),
                        TaskaRunName = c.String(nullable: false, maxLength: 20),
                        Status = c.Int(nullable: false),
                        TaskaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskaRunId)
                .ForeignKey("dbo.TaskaRuns", t => t.ParentTaskaRunId)
                .ForeignKey("dbo.Taskas", t => t.TaskaId)
                .Index(t => t.ParentTaskaRunId)
                .Index(t => t.TaskaRunName, unique: true, name: "AK_Taska_TaskaRunName")
                .Index(t => t.TaskaId);
            
            CreateTable(
                "dbo.Taskas",
                c => new
                    {
                        TaskaId = c.Int(nullable: false, identity: true),
                        ParentTaskaId = c.Int(),
                        TaskaName = c.String(nullable: false, maxLength: 20),
                        TaskaDescription = c.String(),
                    })
                .PrimaryKey(t => t.TaskaId)
                .ForeignKey("dbo.Taskas", t => t.ParentTaskaId)
                .Index(t => t.ParentTaskaId)
                .Index(t => t.TaskaName, unique: true, name: "AK_Taska_TaskaName");
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Messages", "TaskaRunId", "dbo.TaskaRuns");
            DropForeignKey("dbo.TaskaRuns", "TaskaId", "dbo.Taskas");
            DropForeignKey("dbo.Taskas", "ParentTaskaId", "dbo.Taskas");
            DropForeignKey("dbo.TaskaRuns", "ParentTaskaRunId", "dbo.TaskaRuns");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Taskas", "AK_Taska_TaskaName");
            DropIndex("dbo.Taskas", new[] { "ParentTaskaId" });
            DropIndex("dbo.TaskaRuns", new[] { "TaskaId" });
            DropIndex("dbo.TaskaRuns", "AK_Taska_TaskaRunName");
            DropIndex("dbo.TaskaRuns", new[] { "ParentTaskaRunId" });
            DropIndex("dbo.Messages", new[] { "TaskaRunId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Taskas");
            DropTable("dbo.TaskaRuns");
            DropTable("dbo.Messages");
        }
    }
}
