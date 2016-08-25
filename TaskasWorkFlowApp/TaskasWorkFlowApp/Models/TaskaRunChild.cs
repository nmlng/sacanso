using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Web;


namespace TaskasWorkFlowApp.Models
{
  public class TaskaRunChild
  {
    [Required]
    public int Order { get; set; }

    public int ParentTaskaRunId { get; set; }
    public virtual TaskaRun ParentTaskaRun { get; set; }

    public int ChildTaskaRunId { get; set; }
    public virtual TaskaRun ChildTaskaRun { get; set; }
  }
}