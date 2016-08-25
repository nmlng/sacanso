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
  public class NoteRunConfiguration : EntityTypeConfiguration<NoteRun>
  {
    public NoteRunConfiguration()
    {
      Property(m => m.Id).HasColumnName("NoteRunId");

      HasRequired(c => c.TaskaRun)
        .WithMany(c => c.Notes)
        .WillCascadeOnDelete(false);

    }
  }
}