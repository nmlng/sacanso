using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskWebApplication.Models;

namespace TaskWebApplication.Controllers
{
	public class JobResultsController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: JobResults
		public ActionResult Index(int? id)
		{
			if (id != null)
			{
				var jobResults = db.JobResults.Include(s => s.ParentJob)
					.Where(item => item.JobId == id)
					.OrderBy(x => x.JobId)
					.ThenBy(x => x.Id);
				return View(jobResults.ToList());
			}
			else
			{
				var jobResults = db.JobResults.Include(s => s.ParentJob)
					.OrderBy(x => x.JobId)
					.ThenBy(x => x.Id);
				return View(jobResults.ToList());
				//var jobResults = db.JobResults.Include(j => j.ParentJob);
				//return View(jobResults.ToList());
			}
		}

		// GET: JobResults/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			JobResult jobResult = db.JobResults.Find(id);
			if (jobResult == null)
			{
				return HttpNotFound();
			}
			return View(jobResult);
		}

		// GET: JobResults/Create
		public ActionResult Create()
		{
			ViewBag.JobId = new SelectList(db.Jobs, "Id", "JobName");
			return View();
		}

		// POST: JobResults/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,JobId,Description,Command,ErrorMessage,Status")] JobResult jobResult)
		{
			if (ModelState.IsValid)
			{
				db.JobResults.Add(jobResult);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.JobId = new SelectList(db.Jobs, "Id", "JobName", jobResult.JobId);
			return View(jobResult);
		}

		// GET: JobResults/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			JobResult jobResult = db.JobResults.Find(id);
			if (jobResult == null)
			{
				return HttpNotFound();
			}
			ViewBag.JobId = new SelectList(db.Jobs, "Id", "JobName", jobResult.JobId);
			return View(jobResult);
		}

		// POST: JobResults/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,JobId,Description,Command,ErrorMessage,Status")] JobResult jobResult)
		{
			if (ModelState.IsValid)
			{
				db.Entry(jobResult).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.JobId = new SelectList(db.Jobs, "Id", "JobName", jobResult.JobId);
			return View(jobResult);
		}

		// GET: JobResults/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			JobResult jobResult = db.JobResults.Find(id);
			if (jobResult == null)
			{
				return HttpNotFound();
			}
			return View(jobResult);
		}

		// POST: JobResults/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			JobResult jobResult = db.JobResults.Find(id);
			db.JobResults.Remove(jobResult);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		// GET: JobResults/DeleteAll/5
		public ActionResult DeleteAll()
		{
			List<JobResult> jobResults = db.JobResults.ToList();
			foreach (JobResult jobResult in jobResults)
			{
				db.JobResults.Remove(jobResult);
			}
			db.SaveChanges();
			return RedirectToAction("Index");
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
