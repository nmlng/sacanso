using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TaskasWorkFlowApp.Models;

namespace TaskasWorkFlowApp.DataLayer
{
  public class TaskaRunChildConfiguration : EntityTypeConfiguration<TaskaRunChild>
  {
    public TaskaRunChildConfiguration()
    {
      HasKey(t => new { t.ParentTaskaRunId, t.ChildTaskaRunId });

      HasRequired(c => c.ParentTaskaRun)
        .WithMany(c => c.Children)
        .WillCascadeOnDelete(false);

      HasRequired(c => c.ChildTaskaRun);

    }
  }
}