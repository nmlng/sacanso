using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TaskasWorkFlowApp.Models
{
  public class TaskaRun 
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "You must enter a taskaRun name.")]
    [StringLength(256, ErrorMessage = "TaskaRun names must be 256 characters or shorter.")]
    [Display(Name = "TaskaRun")]
    public string TaskaRunName { get; set; }
    public Status Status { get; set; }

    public virtual Taska Taska { get; set; }
    [Display(Name = "Taska")]
    public int TaskaId { get; set; }

    public ICollection<TaskaRunChild> Children { get; set; }
    public ICollection<NoteRun> Notes { get; set; }

  }

  public enum Status
  {
    Running,
    Executed,
    Aborted,
  }
}