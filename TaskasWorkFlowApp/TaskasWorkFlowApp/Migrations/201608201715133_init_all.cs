namespace TaskasWorkFlowApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_all : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        NoteText = c.String(),
                    })
                .PrimaryKey(t => t.NoteId);
            
            CreateTable(
                "dbo.Taskas",
                c => new
                    {
                        TaskaId = c.Int(nullable: false, identity: true),
                        TaskaName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TaskaId);
            
            CreateTable(
                "dbo.TaskaRuns",
                c => new
                    {
                        TaskaRunId = c.Int(nullable: false, identity: true),
                        TaskaRunName = c.String(nullable: false, maxLength: 20),
                        Status = c.Int(nullable: false),
                        TaskaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskaRunId)
                .ForeignKey("dbo.Taskas", t => t.TaskaId, cascadeDelete: true)
                .Index(t => t.TaskaId);
            
            CreateTable(
                "dbo.ParameterRuns",
                c => new
                    {
                        ParameterRunId = c.Int(nullable: false, identity: true),
                        ParameterValue = c.Int(nullable: false),
                        ParameterId = c.Int(nullable: false),
                        TaskaRunId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParameterRunId)
                .ForeignKey("dbo.Parameters", t => t.ParameterId)
                .ForeignKey("dbo.TaskaRuns", t => t.TaskaRunId)
                .Index(t => t.ParameterId)
                .Index(t => t.TaskaRunId);
            
            CreateTable(
                "dbo.Parameters",
                c => new
                    {
                        ParameterId = c.Int(nullable: false, identity: true),
                        ParameterName = c.String(),
                        ParameterDescription = c.String(),
                        ParameterType = c.Int(nullable: false),
                        TaskaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParameterId)
                .ForeignKey("dbo.Taskas", t => t.TaskaId)
                .Index(t => t.TaskaId);
            
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
            
            CreateTable(
                "dbo.TaskaNotes",
                c => new
                    {
                        NoteId = c.Int(nullable: false),
                        TaskaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NoteId, t.TaskaId })
                .ForeignKey("dbo.Notes", t => t.NoteId, cascadeDelete: true)
                .ForeignKey("dbo.Taskas", t => t.TaskaId, cascadeDelete: true)
                .Index(t => t.NoteId)
                .Index(t => t.TaskaId);
            
            CreateTable(
                "dbo.ParentChildTaskaRun",
                c => new
                    {
                        ChildTaskaRunId = c.Int(nullable: false),
                        ParentTaskaRunId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ChildTaskaRunId, t.ParentTaskaRunId })
                .ForeignKey("dbo.TaskaRuns", t => t.ChildTaskaRunId)
                .ForeignKey("dbo.TaskaRuns", t => t.ParentTaskaRunId)
                .Index(t => t.ChildTaskaRunId)
                .Index(t => t.ParentTaskaRunId);
            
            CreateTable(
                "dbo.TaskaRunNotes",
                c => new
                    {
                        NoteId = c.Int(nullable: false),
                        TaskaRunId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NoteId, t.TaskaRunId })
                .ForeignKey("dbo.Notes", t => t.NoteId, cascadeDelete: true)
                .ForeignKey("dbo.TaskaRuns", t => t.TaskaRunId, cascadeDelete: true)
                .Index(t => t.NoteId)
                .Index(t => t.TaskaRunId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ParameterRuns", "TaskaRunId", "dbo.TaskaRuns");
            DropForeignKey("dbo.ParameterRuns", "ParameterId", "dbo.Parameters");
            DropForeignKey("dbo.Parameters", "TaskaId", "dbo.Taskas");
            DropForeignKey("dbo.TaskaRunNotes", "TaskaRunId", "dbo.TaskaRuns");
            DropForeignKey("dbo.TaskaRunNotes", "NoteId", "dbo.Notes");
            DropForeignKey("dbo.TaskaRuns", "TaskaId", "dbo.Taskas");
            DropForeignKey("dbo.ParentChildTaskaRun", "ParentTaskaRunId", "dbo.TaskaRuns");
            DropForeignKey("dbo.ParentChildTaskaRun", "ChildTaskaRunId", "dbo.TaskaRuns");
            DropForeignKey("dbo.TaskaNotes", "TaskaId", "dbo.Taskas");
            DropForeignKey("dbo.TaskaNotes", "NoteId", "dbo.Notes");
            DropForeignKey("dbo.ParentChildTaska", "ParentTaskaId", "dbo.Taskas");
            DropForeignKey("dbo.ParentChildTaska", "ChildTaskaId", "dbo.Taskas");
            DropIndex("dbo.TaskaRunNotes", new[] { "TaskaRunId" });
            DropIndex("dbo.TaskaRunNotes", new[] { "NoteId" });
            DropIndex("dbo.ParentChildTaskaRun", new[] { "ParentTaskaRunId" });
            DropIndex("dbo.ParentChildTaskaRun", new[] { "ChildTaskaRunId" });
            DropIndex("dbo.TaskaNotes", new[] { "TaskaId" });
            DropIndex("dbo.TaskaNotes", new[] { "NoteId" });
            DropIndex("dbo.ParentChildTaska", new[] { "ParentTaskaId" });
            DropIndex("dbo.ParentChildTaska", new[] { "ChildTaskaId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Parameters", new[] { "TaskaId" });
            DropIndex("dbo.ParameterRuns", new[] { "TaskaRunId" });
            DropIndex("dbo.ParameterRuns", new[] { "ParameterId" });
            DropIndex("dbo.TaskaRuns", new[] { "TaskaId" });
            DropTable("dbo.TaskaRunNotes");
            DropTable("dbo.ParentChildTaskaRun");
            DropTable("dbo.TaskaNotes");
            DropTable("dbo.ParentChildTaska");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Parameters");
            DropTable("dbo.ParameterRuns");
            DropTable("dbo.TaskaRuns");
            DropTable("dbo.Taskas");
            DropTable("dbo.Notes");
        }
    }
}
