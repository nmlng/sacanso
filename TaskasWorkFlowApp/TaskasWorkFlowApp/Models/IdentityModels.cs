﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TaskasWorkFlowApp.DataLayer;

namespace TaskasWorkFlowApp.Models
{
  // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
  public class ApplicationUser : IdentityUser
  {
    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    {
      // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
      var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
      // Add custom user claims here
      return userIdentity;
    }
  }

  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext()
        : base("DefaultConnection", throwIfV1Schema: false)
    {
    }

    public DbSet<Taska> Taskas { get; set; }
    public DbSet<TaskaChild> TaskaChilds { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Parameter> Parameters { get; set; }

    public DbSet<TaskaRun> TaskaRuns { get; set; }
    public DbSet<TaskaRunChild> TaskaRunChilds { get; set; }
    public DbSet<NoteRun> NoteRuns { get; set; }
    public DbSet<ParameterRun> ParameterRuns { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
//      modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

      modelBuilder.Configurations.Add(new TaskaConfiguration());
      modelBuilder.Configurations.Add(new TaskaChildConfiguration()); 
      modelBuilder.Configurations.Add(new TaskaRunConfiguration());
      modelBuilder.Configurations.Add(new TaskaRunChildConfiguration());
      modelBuilder.Configurations.Add(new NoteConfiguration());
      modelBuilder.Configurations.Add(new NoteRunConfiguration());
      modelBuilder.Configurations.Add(new ParameterConfiguration());
      modelBuilder.Configurations.Add(new ParameterRunConfiguration());


      base.OnModelCreating(modelBuilder);
    }

    public static ApplicationDbContext Create()
    {
      return new ApplicationDbContext();
    }
  }
}