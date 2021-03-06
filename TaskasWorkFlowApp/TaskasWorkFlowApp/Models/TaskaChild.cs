﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Web;


namespace TaskasWorkFlowApp.Models
{
  public class TaskaChild
  {
    [Required]
    public int Order { get; set; }

    public int ParentTaskaId { get; set; }
    [Display(Name = "ParentTaska")]
    public virtual Taska ParentTaska { get; set; }


    public int ChildTaskaId { get; set; }
    [Display(Name = "ChildTaska")]
    public virtual Taska ChildTaska { get; set; }
  }
}