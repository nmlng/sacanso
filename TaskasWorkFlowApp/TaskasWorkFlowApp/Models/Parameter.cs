using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TaskasWorkFlowApp.Models
{
  public class Parameter
  {
    public int Id { get; set; }
    public string ParameterName { get; set; }
    public ParameterType ParameterType { get; set; }

    public virtual Taska Taska { get; set; }
    [Display(Name = "Taska")]
    public int TaskaId { get; set; }
  }

  public enum ParameterType
  {
    Input,
    Output,
    NotDefined
  }
}