using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using TaskWebApplication.Models;

namespace TaskWebApplication.Controllers
{
    public class JobParametersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JobParameters

				public ActionResult Index(int? id)
        {
					if (id != null)
					{
						var jobParameters = db.JobParameters
							.Include(j => j.JobParameter)
							.Include(j => j.ParentJob)
							.Where(item => item.JobId == id)
							.OrderBy(x => x.JobId)
							.ThenBy(x => x.ParameterId);
						return View(jobParameters.ToList());
					}
					else
					{
						var jobParameters = db.JobParameters
							.Include(j => j.JobParameter)
							.Include(j => j.ParentJob)
							.OrderBy(x => x.JobId)
							.ThenBy(x => x.ParameterId);
						return View(jobParameters.ToList());
					}
        }

        // GET: JobParameters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobParameters jobParameters = db.JobParameters.Find(id);
            if (jobParameters == null)
            {
                return HttpNotFound();
            }
            return View(jobParameters);
        }

        // GET: JobParameters/Create
        public ActionResult Create(int? jobId)
        {
            ViewBag.ParameterId = new SelectList(db.Parameters, "Id", "ParameterName");
						ViewBag.JobId = new SelectList(db.Jobs, "Id", "JobName", jobId);
            return View();
        }

        // POST: JobParameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,JobId,ParameterId,ParameterValue")] JobParameters jobParameters)
        {
            if (ModelState.IsValid)
            {
                db.JobParameters.Add(jobParameters);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParameterId = new SelectList(db.Parameters, "Id", "ParameterName", jobParameters.ParameterId);
            ViewBag.JobId = new SelectList(db.Jobs, "Id", "JobName", jobParameters.JobId);
            return View(jobParameters);
        }

        // GET: JobParameters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobParameters jobParameters = db.JobParameters.Find(id);
            if (jobParameters == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParameterId = new SelectList(db.Parameters, "Id", "ParameterName", jobParameters.ParameterId);
            ViewBag.JobId = new SelectList(db.Jobs, "Id", "JobName", jobParameters.JobId);
            return View(jobParameters);
        }

        // POST: JobParameters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,JobId,ParameterId,ParameterValue")] JobParameters jobParameters)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobParameters).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParameterId = new SelectList(db.Parameters, "Id", "ParameterName", jobParameters.ParameterId);
            ViewBag.JobId = new SelectList(db.Jobs, "Id", "JobName", jobParameters.JobId);
            return View(jobParameters);
        }

        // GET: JobParameters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobParameters jobParameters = db.JobParameters.Find(id);
            if (jobParameters == null)
            {
                return HttpNotFound();
            }
            return View(jobParameters);
        }

        // POST: JobParameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobParameters jobParameters = db.JobParameters.Find(id);
            db.JobParameters.Remove(jobParameters);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

				public class ParemeterForSelect
				{
					public string id;
					public string value;
				}


				public JsonResult GetJobParameters(int? id)
				{
					if (id == null)
						return Json("");
					var taskaIds = db.Jobs.Select(c => new
					{
						c.Id,
						c.TaskaId
					}).Where(item => item.Id == id);


					var y = taskaIds.ToList().First().TaskaId;

					var parameters = db.Parameters.Select(c => new
					{
						id = c.Id,
						c.TaskaId,
						value = c.ParameterName
					}).Where(item => item.TaskaId == y);

					List<ParemeterForSelect> result = new List<ParemeterForSelect>();
					foreach (var item in parameters)
					{
						result.Add(new ParemeterForSelect
						{
							id = item.id.ToString(),
							value = item.value.ToString(),
						}
						);
					}

					//			var x = taskaId.Where(item => item.Id.ToString() == id);
					//			var result = Json(parameters, JsonRequestBehavior.AllowGet);
					//			return result;
					return Json(result);
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
