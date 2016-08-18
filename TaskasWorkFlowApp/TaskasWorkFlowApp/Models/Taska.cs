using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TreeUtility;

namespace TaskasWorkFlowApp.Models
{
  public class Taska : ITreeNode<Taska>
  {
    public int Id { get; set; }

    private int? _parentTaskaId;

    [Display(Name = "Parent Taska")]
    public int? ParentTaskaId
    {
      get { return _parentTaskaId; }
      set
      {
        if (Id == value)
          throw new InvalidOperationException("A taska cannot have itself as its parent.");

        _parentTaskaId = value;
      }
    }

    [Required(ErrorMessage = "You must enter a taska name.")]
    [StringLength(20, ErrorMessage = "Taska names must be 20 characters or shorter.")]
    [Display(Name = "Taska")]
    public string TaskaName { get; set; }
    public string TaskaDescription { get; set; }
    public virtual Taska Parent { get; set; }
    public IList<Taska> Children { get; set; }
  }
}