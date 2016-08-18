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
  public class TaskaRunConfiguration : EntityTypeConfiguration<TaskaRun>
  {
    public TaskaRunConfiguration()
    {
      Property(tr => tr.Id).HasColumnName("TaskaRunId");

      Property(tr => tr.TaskaRunName)
          .HasMaxLength(20)
          .IsRequired()
          .HasColumnAnnotation("Index",
              new IndexAnnotation(new IndexAttribute("AK_Taska_TaskaRunName") { IsUnique = true }));

      HasOptional(tr => tr.Parent)
          .WithMany(tr => tr.Children)
          .HasForeignKey(tr => tr.ParentTaskaRunId)
          .WillCascadeOnDelete(false);

      HasRequired(tr => tr.Taska).WithMany().WillCascadeOnDelete(false);
    }
  }
}