﻿using System;
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
      Property(c => c.Id).HasColumnName("TaskaRunId");
    }
  }
}