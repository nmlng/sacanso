using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TreeUtility;

namespace TaskasWorkFlowApp.Models
{
  public class Message
  {
    public int Id { get; set; }
    public string MessageText { get; set; }
    public MessageType MessageType { get; set; }

    public virtual TaskaRun TaskaRun { get; set; }
    [Display(Name = "TaskaRun")]
    public int TaskaRunId { get; set; }
  }

  public enum MessageType
  {
    StartMessage,
    EndMessage,
    Note,
  }
}