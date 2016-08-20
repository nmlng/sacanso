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
  public class NoteConfiguration : EntityTypeConfiguration<Note>
  {
    public NoteConfiguration()
    {
      Property(m => m.Id).HasColumnName("NoteId");

      
      HasMany(c => c.Taskas)
     .WithMany(c => c.Notes)
     .Map(m =>
     {
       m.MapLeftKey("NoteId");
       m.MapRightKey("TaskaId");
       m.ToTable("TaskaNotes");
     });

      HasMany(c => c.TaskasRuns)
      .WithMany(c => c.Notes)
      .Map(m =>
      {
        m.MapLeftKey("NoteId");
        m.MapRightKey("TaskaRunId");
        m.ToTable("TaskaRunNotes");
      });

    }
  }
}