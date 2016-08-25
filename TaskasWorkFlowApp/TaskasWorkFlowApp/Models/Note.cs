﻿using System;
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

    public int TaskaId { get; set; }
    public virtual Taska Taska { get; set; }

  }

}