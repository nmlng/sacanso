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
  public class ParameterConfiguration : EntityTypeConfiguration<Parameter>
  {
    public ParameterConfiguration()
    {
      Property(m => m.Id).HasColumnName("ParameterId");

      HasRequired(m => m.Taska).WithMany().WillCascadeOnDelete(false);

    }
  }
}