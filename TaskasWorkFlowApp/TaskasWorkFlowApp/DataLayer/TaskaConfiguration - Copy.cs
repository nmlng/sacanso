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
  public class TaskaConfiguration : EntityTypeConfiguration<Taska>
  {
    public TaskaConfiguration()
    {
      Property(c => c.Id).HasColumnName("TaskaId");

      Property(c => c.TaskaName)
          .HasMaxLength(20)
          .IsRequired()
          .HasColumnAnnotation("Index",
              new IndexAnnotation(new IndexAttribute("AK_Taska_TaskaName") { IsUnique = true }));

      HasOptional(c => c.Parent)
          .WithMany(c => c.Children)
          .HasForeignKey(c => c.ParentTaskaId)
          .WillCascadeOnDelete(false);
    }
  }
}