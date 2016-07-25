﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskWebApplication.Models
{

	public enum Status
	{
		READY, ONGOING, WAITING, SUCCESS, ERROR
	}


	public class Taska
	{
		public int Id { get; set; }

		[DisplayName("Taska Description")]
		[Required]
		public string Description { get; set; }
		public Status? Status { get; set; }

		public virtual ICollection<SubTaska> SubTaskas { get; set; }
		public virtual ICollection<Parameter> Parameters { get; set; }

		public Taska()
		{
			this.Status = Models.Status.READY;
		}
	}

	public class Parameter
	{
		public int Id { get; set; }

		[DisplayName("Parameter Name")]
		public string ParameterName { get; set; }

		[DisplayName("Parameter Value")]
		public string ParameterValue { get; set; }

		[ForeignKey("ParameterTaska")]
		public int TaskaId { get; set; }
		public virtual Taska ParameterTaska { get; set; }

		public virtual ICollection<JobParameters> JobParameters { get; set; }

	}


	public class SubTaska
	{
		public int Id { get; set; }

		[DisplayName("Sub-Taska Description")]
		public string Description { get; set; }
		public string Command { get; set; }

		[DisplayName("Message")]
		[DisplayFormat(NullDisplayText = "")]
		public string ErrorMessage { get; set; }
		public Status? Status { get; set; }


		[ForeignKey("ParentTaska")]
		public int TaskaId { get; set; }
		public virtual Taska ParentTaska { get; set; }

		public SubTaska()
		{
			this.Status = Models.Status.READY;
		}
	}

	public class Job
	{
		public int Id { get; set; }

		[DisplayName("Job Name")]
    [Required]
		public string JobName { get; set; }

		public Status? Status { get; set; }

		[ForeignKey("CorrespondingTaska")]
		public int TaskaId { get; set; }
		public virtual Taska CorrespondingTaska { get; set; }

		public virtual ICollection<JobResult> JobResults { get; set; }

		public Job()
		{
			this.Status = Models.Status.READY;
		}
	}

	public class JobParameters
	{
		public int Id { get; set; }

		[Required]
		[ForeignKey("ParentJob")]
		public int JobId { get; set; }
		public virtual Job ParentJob { get; set; }

		[Required]
    [ForeignKey("JobParameter")]
		public int ParameterId { get; set; }
		public virtual Parameter JobParameter { get; set; }
    [DisplayName("Parameter Value")]
    public string ParameterValue { get; set; }
	}

		public class JobResult
		{
			public int Id { get; set; }

			[Required]
			[ForeignKey("ParentJob")]
			public int JobId { get; set; }
			public virtual Job ParentJob { get; set; }

			[DisplayName("Sub-Taska Description")]
			public string Description { get; set; }
			public string Command { get; set; }

			[DisplayName("Message")]
			[DisplayFormat(NullDisplayText = "")]
			public string ErrorMessage { get; set; }
			public Status? Status { get; set; }
		}

}