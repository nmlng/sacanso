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
	public class ParametersController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: Parameters
		public ActionResult Index(int? id)
		{
			if (id != null)
			{
				var parameters = db.Parameters.Include(p => p.ParameterTaska)
					.Where(item => item.TaskaId == id)
					.OrderBy(x => x.TaskaId)
					.ThenBy(x => x.ParameterName);
				return View(parameters.ToList());
			}
			else
			{
				var parameters = db.Parameters.Include(p => p.ParameterTaska)
					.OrderBy(x => x.TaskaId)
					.ThenBy(x => x.ParameterName);
				return View(parameters.ToList());
			}
		}

		// GET: Parameters/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Parameter parameter = db.Parameters.Find(id);
			if (parameter == null)
			{
				return HttpNotFound();
			}
			return View(parameter);
		}

		// GET: Parameters/Create
		public ActionResult Create()
		{
			ViewBag.TaskaId = new SelectList(db.Taskas, "Id", "Description");
			return View();
		}

		// POST: Parameters/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,ParameterName,ParameterValue,TaskaId")] Parameter parameter)
		{
			if (ModelState.IsValid)
			{
				db.Parameters.Add(parameter);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.TaskaId = new SelectList(db.Taskas, "Id", "Description", parameter.TaskaId);
			return View(parameter);
		}

		// GET: Parameters/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Parameter parameter = db.Parameters.Find(id);
			if (parameter == null)
			{
				return HttpNotFound();
			}
			ViewBag.TaskaId = new SelectList(db.Taskas, "Id", "Description", parameter.TaskaId);
			return View(parameter);
		}

		// POST: Parameters/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,ParameterName,ParameterValue,TaskaId")] Parameter parameter)
		{
			if (ModelState.IsValid)
			{
				db.Entry(parameter).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.TaskaId = new SelectList(db.Taskas, "Id", "Description", parameter.TaskaId);
			return View(parameter);
		}

		// GET: Parameters/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Parameter parameter = db.Parameters.Find(id);
			if (parameter == null)
			{
				return HttpNotFound();
			}
			return View(parameter);
		}

		// POST: Parameters/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Parameter parameter = db.Parameters.Find(id);
			db.Parameters.Remove(parameter);
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
