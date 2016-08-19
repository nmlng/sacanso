using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TaskasWorkFlowApp.Models
{
  public class Note
  {
    public int Id { get; set; }
    public string NoteText { get; set; }

    public virtual Taska Taska { get; set; }
    [Display(Name = "Taska")]
    public int TaskaId { get; set; }

    public virtual TaskaRun TaskaRun { get; set; }
    [Display(Name = "TaskaRun")]
    public int TaskaRunId { get; set; }
  }

}