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
  public class ParameterRunConfiguration : EntityTypeConfiguration<ParameterRun>
  {
    public ParameterRunConfiguration()
    {
      Property(m => m.Id).HasColumnName("ParameterRunId");

      HasRequired(m => m.Parameter).WithMany().WillCascadeOnDelete(false);
      HasRequired(m => m.TaskaRun).WithMany().WillCascadeOnDelete(false);

    }
  }
}