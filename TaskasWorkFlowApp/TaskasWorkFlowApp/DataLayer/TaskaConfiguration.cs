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

      //HasOptional(c => c.ChildTaskas)
      //  .WithRequired();

      // HasMany(c => c.ParenTaskas)
      //.WithMany(c => c.ChildTaskas)
      //.Map(m =>
      //{
      //  m.MapLeftKey("ChildTaskaId");
      //  m.MapRightKey("ParentTaskaId");
      //  m.ToTable("ParentChildTaska");
      //});
    }
  }
}