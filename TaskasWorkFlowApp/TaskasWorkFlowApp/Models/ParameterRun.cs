using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TaskasWorkFlowApp.Models
{
  public class ParameterRun
  {
    public int Id { get; set; }
    public ParameterType ParameterValue { get; set; }
    public virtual Parameter Parameter { get; set; }
    [Display(Name = "Parameter")]
    public int ParameterId { get; set; }
    public virtual TaskaRun TaskaRun { get; set; }
    [Display(Name = "TaskaRun")]
    public int TaskaRunId { get; set; }
  }

}