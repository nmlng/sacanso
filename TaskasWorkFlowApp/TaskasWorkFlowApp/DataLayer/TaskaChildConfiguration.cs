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
  public class TaskaChildConfiguration : EntityTypeConfiguration<TaskaChild>
  {
    public TaskaChildConfiguration()
    {
      HasKey(t => new { t.ParentTaskaId, t.ChildTaskaId });

      HasRequired(c => c.ParentTaska)
        .WithMany(c => c.Children).WillCascadeOnDelete(false);      
    }
  }
}