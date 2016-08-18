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
  public class MessageConfiguration : EntityTypeConfiguration<Message>
  {
    public MessageConfiguration()
    {
      Property(m => m.Id).HasColumnName("MessageId");

      HasRequired(m => m.TaskaRun).WithMany().WillCascadeOnDelete(false);

    }
  }
}