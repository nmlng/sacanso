using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TreeUtility;

namespace TaskasWorkFlowApp.Models
{
  public class Taska
  {
    public Taska()
    {
      ParenTaskas = new HashSet<Taska>();
      ChildTaskas = new HashSet<Taska>();
    }
    public int TaskaId { get; set; }
    public string TaskaName { get; set; }
    public string Description { get; set; }
    public ICollection<Taska> ParenTaskas { get; set; }
    public ICollection<Taska> ChildTaskas { get; set; }
  }
}