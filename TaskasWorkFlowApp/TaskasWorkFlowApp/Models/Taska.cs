using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TaskasWorkFlowApp.Models
{
  public class Taska
  {
    public int Id { get; set; }
    public string TaskaName { get; set; }
    public string Description { get; set; }
    public ICollection<TaskaChild> Children { get; set; }
    public ICollection<Note> Notes { get; set; }
  }
}