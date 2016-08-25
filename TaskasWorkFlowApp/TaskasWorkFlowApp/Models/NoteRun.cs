using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TaskasWorkFlowApp.Models
{
  public class NoteRun
  {
    public int Id { get; set; }
    public string NoteText { get; set; }

    public int TaskaRunId { get; set; }
    public virtual TaskaRun TaskaRun { get; set; }
  }

}