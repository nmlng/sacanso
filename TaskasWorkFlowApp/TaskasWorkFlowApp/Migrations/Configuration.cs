using TaskasWorkFlowApp.Models;

namespace TaskasWorkFlowApp.Migrations
{
  using System;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Linq;

  internal sealed class Configuration : DbMigrationsConfiguration<TaskasWorkFlowApp.Models.ApplicationDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(TaskasWorkFlowApp.Models.ApplicationDbContext context)
    {
      //  This method will be called after migrating to the latest version.

      //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
      //  to avoid creating duplicate seed data. E.g.
      //
      //    context.People.AddOrUpdate(
      //      p => p.FullName,
      //      new Person { FullName = "Andrew Peters" },
      //      new Person { FullName = "Brice Lambson" },
      //      new Person { FullName = "Rowan Miller" }
      //    );
      //

      context.Taskas.AddOrUpdate(
          t => t.TaskaName,
          new Taska { TaskaName = "Taska1", Description = "Descreve-mos ", },
          new Taska { TaskaName = "Taska2", Description = "F1  " },
          new Taska { TaskaName = "Taska3", Description = "F2 " }
          );

      context.SaveChanges();

      var taskaMae = context.Taskas.Single(t => t.TaskaName == "Taska1");
      var taskasFilhas = context.Taskas.Where(c => c.Description.StartsWith("F")).ToList();
      taskaMae.ChildTaskas = taskasFilhas;

      var taskaMae2 = context.Taskas.Single(t => t.TaskaName == "Taska2");
      var taskasFilha2 = context.Taskas.Where(c => c.Description.StartsWith("F2")).ToList();
      taskaMae2.ChildTaskas = taskasFilha2;


    }
  }
}
