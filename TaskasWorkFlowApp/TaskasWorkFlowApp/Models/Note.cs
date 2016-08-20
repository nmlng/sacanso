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

    public virtual ICollection<Taska> Taskas { get; set; }

    public virtual ICollection<TaskaRun> TaskasRuns { get; set; }
  }

}