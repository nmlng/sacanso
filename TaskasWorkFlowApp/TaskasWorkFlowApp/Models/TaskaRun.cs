using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TreeUtility;

namespace TaskasWorkFlowApp.Models
{
  public class TaskaRun : ITreeNode<TaskaRun>
  {
    public int Id { get; set; }

    private int? _parentTaskaRunId;

    [Display(Name = "Parent TaskaRun")]
    public int? ParentTaskaRunId
    {
      get { return _parentTaskaRunId; }
      set
      {
        if (Id == value)
          throw new InvalidOperationException("A taskaRun cannot have itself as its parent.");

        _parentTaskaRunId = value;
      }
    }

    [Required(ErrorMessage = "You must enter a taska name.")]
    [StringLength(20, ErrorMessage = "Taska names must be 20 characters or shorter.")]
    [Display(Name = "TaskaRun")]
    public string TaskaRunName { get; set; }
    public Status Status { get; set; }
    public virtual TaskaRun Parent { get; set; }
    public IList<TaskaRun> Children { get; set; }


    public virtual Taska Taska { get; set; }
    [Display(Name = "Taska")]
    public int TaskaId { get; set; }
  }

  public enum Status
  {
    Running,
    Executed,
    Aborted,
  }
}