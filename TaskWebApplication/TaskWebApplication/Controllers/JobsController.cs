using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TaskWebApplication.Models;

namespace TaskWebApplication.Controllers
{
  public class JobsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Jobs
    public ActionResult Index()
    {
      var jobs = db.Jobs.Include(j => j.CorrespondingTaska)
        .OrderBy(x => x.JobName)
        .ThenBy(x => x.CorrespondingTaska);
      return View(jobs.ToList());
    }

    // GET: Jobs/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Job job = db.Jobs.Find(id);
      if (job == null)
      {
        return HttpNotFound();
      }
      return View(job);
    }

    // GET: Jobs/Create
    public ActionResult Create()
    {
      ViewBag.TaskaId = new SelectList(db.Taskas, "Id", "Description");
      return View();
    }

    // POST: Jobs/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,JobName,TaskaId")] Job job)
    {
      if (ModelState.IsValid)
      {
        db.Jobs.Add(job);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      ViewBag.TaskaId = new SelectList(db.Taskas, "Id", "Description", job.TaskaId);
      return View(job);
    }

    // GET: Jobs/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Job job = db.Jobs.Find(id);
      if (job == null)
      {
        return HttpNotFound();
      }
      ViewBag.TaskaId = new SelectList(db.Taskas, "Id", "Description", job.TaskaId);
      return View(job);
    }

    // POST: Jobs/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,JobName,TaskaId")] Job job)
    {
      if (ModelState.IsValid)
      {
        db.Entry(job).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      ViewBag.TaskaId = new SelectList(db.Taskas, "Id", "Description", job.TaskaId);
      return View(job);
    }

    // GET: Jobs/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Job job = db.Jobs.Find(id);
      if (job == null)
      {
        return HttpNotFound();
      }
      return View(job);
    }

    // POST: Jobs/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Job job = db.Jobs.Find(id);
      db.Jobs.Remove(job);
      db.SaveChanges();
      return RedirectToAction("Index");
    }


    public ActionResult ResetJob(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Job job = db.Jobs.Find(id);
      if (job == null)
      {
        return HttpNotFound();
      }

      var jobResults = GetResultsForJob(id);

      //			var subTaskas = db.SubTaskas.SqlQuery("select * from SubTaskas where TaskaId = " + id.ToString());
      foreach (JobResult jobResult in jobResults)
      {
        jobResult.Status = Status.READY;
        jobResult.ErrorMessage = String.Empty;
      }

      job.Status = Status.READY;
      db.Entry(job).State = EntityState.Modified;
      db.SaveChanges();

      return RedirectToAction("Index");
    }


    public ActionResult ResetAll()
    {
      List<Job> jobs = db.Jobs.ToList();

      foreach (Job job in jobs)
      {
        job.Status = Status.READY;
      }

      db.SaveChanges();

      return RedirectToAction("Index");
    }


    // GET: Jobs/ApplyJobParameters/5
    public ActionResult ApplyJobParameters(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Job job = db.Jobs.Find(id);
      if (job == null)
      {
        return HttpNotFound();
      }

      var taskaControler = new TaskasController();
      var taskaId = db.Jobs.Include(j => j.CorrespondingTaska)
                     .Where(item => item.Id == id)
                     .ToList().Last().TaskaId;
      //			var taskaId = taskaIds.ToList().Last().TaskaId;
      var subTaskas = taskaControler.GetTaskaSubTaskas(taskaId);
      var taskaParameters = db.Parameters.Include(p => p.ParameterTaska)
                       .Where(item => item.TaskaId == taskaId);

      var jobParameters = GetJobParameters(id);
      foreach (SubTaska subtasca in subTaskas)
      {
        var jobResult = CreateJobResult(id, subtasca);
        var newCommand = UpdateCommand(jobResult.Command, taskaParameters, jobParameters);
        jobResult.Command = newCommand;
        db.JobResults.Add(jobResult);
      }

      job.Status = Status.READY;
      db.Entry(job).State = EntityState.Modified;
      db.SaveChanges();
      return null;
    }

    private static string UpdateCommand(string command, IQueryable<Parameter> taskaParameters, Dictionary<string, string> jobParameters)
    {
      if (command != null) 
        foreach (Parameter parameter in taskaParameters)
        {
          string pattern = @"(\{{2}\s*" + parameter.ParameterName + @"\s*\}{2})";
          string efectiveValue = jobParameters.ContainsKey(parameter.ParameterName) ?
            jobParameters[parameter.ParameterName] : parameter.ParameterValue;
          if (Regex.Matches(command, pattern).Count > 0)
            command = Regex.Replace(command, pattern, efectiveValue);
        }
      return command;
    }

    private Dictionary<string, string> GetJobParameters(int? id)
    {
      var jobParameters = db.JobParameters.Include(j => j.ParentJob)
        .Where(item => item.JobId == id);

      Dictionary<string, string> jobParametersDict = new Dictionary<string, string>();
      foreach (JobParameters jobpar in jobParameters)
      {
        var paramIds = db.Parameters.Include(p => p.JobParameters)
          .Where(item => item.Id == jobpar.ParameterId);
        var paramName = paramIds.ToList().Last().ParameterName;
        jobParametersDict.Add(paramName, jobpar.ParameterValue);
      }
      return jobParametersDict;
    }

    private static JobResult CreateJobResult(int? id, SubTaska subtasca)
    {
      var jobResult = new JobResult();
      jobResult.JobId = (int)id;
      jobResult.Description = subtasca.Description;
      jobResult.Command = subtasca.Command;
      jobResult.ErrorMessage = String.Empty;
      jobResult.Status = Status.READY;
      return jobResult;
    }

    private IOrderedQueryable<JobResult> GetResultsForJob(int? id)
    {
      var jobResults = db.JobResults.Include(j => j.ParentJob)
        .Where(item => item.JobId == id)
        .OrderBy(x => x.JobId)
        .ThenBy(x => x.Id);
      return jobResults;
    }


    // GET: Jobs/Run/5
    public ActionResult RunJob(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Job job = db.Jobs.Find(id);
      if (job == null)
      {
        return HttpNotFound();
      }

      ApplyJobParameters(id);
      job.Status = Status.ONGOING;
      db.Entry(job).State = EntityState.Modified;
      db.SaveChanges();
      var result = RunJobCommands(id);
      ViewData["id"] = job.Id;
      ViewData["description"] = job.CorrespondingTaska;
      //          return View(db.Taskas.ToList());
      return RedirectToAction("Index");
    }


    public Tuple<string, string, int> RunCommanOnSever(string command)
    {
      Process p = new Process();
      p.StartInfo.FileName = "cmd.exe";
      p.StartInfo.Arguments = "/C " + command;
      p.StartInfo.RedirectStandardOutput = true;
      p.StartInfo.RedirectStandardError = true;
      p.StartInfo.UseShellExecute = false;
      string stdout = String.Empty;
      string stderr = String.Empty;
      int returned = 0;

      try
      {
        p.Start();
        stdout = p.StandardOutput.ReadToEnd();
        stderr = p.StandardError.ReadToEnd();
        p.WaitForExit();
        returned = p.ExitCode;
      }
      catch (Exception e)
      {
        stderr = e.Message;
        returned = -1;
      }
      return new Tuple<string, string, int>(stdout, stderr, returned);
    }


    public ActionResult RunJobCommands(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Job job = db.Jobs.Find(id);
      if (job == null)
      {
        return HttpNotFound();
      }

      var jobResults = GetResultsForJob(id);

      //			var subTaskas = db.SubTaskas.SqlQuery("select * from SubTaskas where TaskaId = " + id.ToString());
      var error = false;
      foreach (JobResult jobResult in jobResults)
      {
        System.Diagnostics.Debug.Write("Running: " + jobResult.Command + "\n");
        Tuple<string, string, int> resultTuple = RunCommanOnSever(jobResult.Command);
        string message = resultTuple.Item1.ToString() + resultTuple.Item2.ToString();
        jobResult.ErrorMessage = message;
        //				System.Diagnostics.Debug.Write(message);
        if (resultTuple.Item3 == 0)
          jobResult.Status = Status.SUCCESS;
        else
        {
          jobResult.Status = Status.ERROR;
          error = true;
          break;
        }
        //				Thread.Sleep(1000);
      }

      job.Status = error ? Status.ERROR : Status.SUCCESS;
      db.Entry(job).State = EntityState.Modified;
      db.SaveChanges();
      //          return View(await db.Taskas.ToListAsync());
      return RedirectToAction("Index", "Jobs");
      //          return null;
    }




    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
