using System.Collections.Generic;
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
      context.Taskas.AddOrUpdate(
            t => t.TaskaName,
            new Taska { TaskaName = "Taska1", Description = "Descreve-mos ", },
            new Taska { TaskaName = "Taska2", Description = "F1  " },
            new Taska { TaskaName = "Taska3", Description = "F2 " },
            new Taska { TaskaName = "Taska4", Description = "E1  " },
            new Taska { TaskaName = "Taska5", Description = "E2 " }
            );

      context.SaveChanges();

      ICollection<TaskaChild> TaskaChildren = new List<TaskaChild>();

      var taskaMae = context.Taskas.Single(t => t.TaskaName == "Taska1");
      var taskaFilha1 = context.Taskas.Single(t => t.TaskaName == "Taska2");
      var taskaFilha2 = context.Taskas.Single(t => t.TaskaName == "Taska3");
      var taskaFilha3 = context.Taskas.Single(t => t.TaskaName == "Taska4");
      var taskaFilha4 = context.Taskas.Single(t => t.TaskaName == "Taska5");

      var childTaska1 = new TaskaChild { Order = 37, ChildTaska = taskaFilha1 };
      TaskaChildren.Add(childTaska1);

      var childTaska2 = new TaskaChild { Order = 69, ChildTaska = taskaFilha2 };
      TaskaChildren.Add(childTaska2);

      var childTaska3 = new TaskaChild { Order = 117, ChildTaska = taskaFilha3 };
      TaskaChildren.Add(childTaska3);

      var childTaska4 = new TaskaChild { Order = 129, ChildTaska = taskaFilha4 };
      TaskaChildren.Add(childTaska4);

      taskaMae.Children = TaskaChildren;

      taskaFilha4.Children = new List<TaskaChild>
      {
        new TaskaChild
        {
          Order = 434,
          ChildTaska = taskaMae
        }
      };

      taskaFilha3.Children = new List<TaskaChild>
      {
        new TaskaChild
        {
          Order = 777,
          ChildTaska = taskaFilha2
        }
      };

       //     context.Taskas.Remove(taskaMae);
      // context.TaskaChilds.Remove(childTaska2);

    }
  }
}
