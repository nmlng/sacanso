using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TreeUtility;

namespace TaskasWorkFlowApp.Models
{
  public class TaskaRun 
  {

    public TaskaRun()
    {
      ParenTaskaRuns = new HashSet<TaskaRun>();
      ChildTaskaRuns = new HashSet<TaskaRun>();
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "You must enter a taskaRun name.")]
    [StringLength(20, ErrorMessage = "TaskaRun names must be 20 characters or shorter.")]
    [Display(Name = "TaskaRun")]
    public string TaskaRunName { get; set; }
    public Status Status { get; set; }

    public virtual Taska Taska { get; set; }
    [Display(Name = "Taska")]
    public int TaskaId { get; set; }

    public ICollection<TaskaRun> ParenTaskaRuns { get; set; }
    public ICollection<TaskaRun> ChildTaskaRuns { get; set; }

  }

  public enum Status
  {
    Running,
    Executed,
    Aborted,
  }
}