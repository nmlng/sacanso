﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TaskWebApplication.Models;

namespace TaskWebApplication.Controllers
{
	public class TaskasController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: Taskas
		public ActionResult Index()
		{
			return View(db.Taskas.ToList());
		}

		// GET: Taskas/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Taska taska = db.Taskas.Find(id);
			if (taska == null)
			{
				return HttpNotFound();
			}
			return View(taska);
		}

		// GET: Taskas/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Taskas/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Description,Status")] Taska taska)
		{
			if (ModelState.IsValid)
			{
				db.Taskas.Add(taska);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(taska);
		}

		// GET: Taskas/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Taska taska = db.Taskas.Find(id);
			if (taska == null)
			{
				return HttpNotFound();
			}
			return View(taska);
		}

		// POST: Taskas/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Description,Status")] Taska taska)
		{
			if (ModelState.IsValid)
			{
				db.Entry(taska).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(taska);
		}

		// GET: Taskas/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Taska taska = db.Taskas.Find(id);
			if (taska == null)
			{
				return HttpNotFound();
			}
			return View(taska);
		}

		// POST: Taskas/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Taska taska = db.Taskas.Find(id);
			db.Taskas.Remove(taska);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public IOrderedQueryable<SubTaska> GetTaskaSubTaskas(int? id)
		{
			var subTaskas = db.SubTaskas.Include(s => s.ParentTaska)
				.Where(item => item.TaskaId == id)
				.OrderBy(x => x.TaskaId)
				.ThenBy(x => x.Id);
			return subTaskas;
		}

		// GET: Taskas/Edit/5
		public ActionResult ResetTaska(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Taska taska = db.Taskas.Find(id);
			if (taska == null)
			{
				return HttpNotFound();
			}

			var subTaskas = GetTaskaSubTaskas(id);

//			var subTaskas = db.SubTaskas.SqlQuery("select * from SubTaskas where TaskaId = " + id.ToString());
			foreach (SubTaska subtasca in subTaskas)
			{
				subtasca.Status = Status.READY;
				subtasca.ErrorMessage = String.Empty;
			}

			taska.Status = Status.READY;
			db.Entry(taska).State = EntityState.Modified;
			db.SaveChanges();
			//        ViewData["id"] = taska.Id;
			//        ViewData["description"] = taska.Description;
			//        return taska.Status;
			//       return View(await db.Taskas.ToListAsync());
			return null;
		}




		// GET: Taskas/Edit/5
		public ActionResult RunTaska(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Taska taska = db.Taskas.Find(id);
			if (taska == null)
			{
				return HttpNotFound();
			}
			ResetTaska(id);
			taska.Status = Status.ONGOING;
			db.Entry(taska).State = EntityState.Modified;
			db.SaveChanges();
			var result = RunTaskCommands(id);
			ViewData["id"] = taska.Id;
			ViewData["description"] = taska.Description;
			//          return View(db.Taskas.ToList());
			return RedirectToAction("Index");
		}


		public ActionResult RunAll()
		{
			var taskas = db.Taskas.ToList();
			foreach (Taska taska in taskas)
			{
				ResetTaska(taska.Id);
				taska.Status = Status.ONGOING;
				db.Entry(taska).State = EntityState.Modified;
				db.SaveChanges();
				RunTaska(taska.Id);
			}
			return RedirectToAction("Index", "Taskas");
		}


		public Tuple<string, string> RunCommanOnSever(string command)
		{
			Process p = new Process();
			p.StartInfo.FileName = "cmd.exe";
			p.StartInfo.Arguments = "/C " + command;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardError = true;
			p.StartInfo.UseShellExecute = false;
			string stdout = String.Empty;
			string stderr = String.Empty;
			try
			{
				p.Start();
				stdout = p.StandardOutput.ReadToEnd();
				stderr = p.StandardError.ReadToEnd();
				p.WaitForExit();
			}
			catch (Exception e)
			{
				stdout = e.Message;
			}
			return new Tuple<string, string>(stdout, stderr);
		}

		public ActionResult RunTaskCommands(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Taska taska = db.Taskas.Find(id);
			if (taska == null)
			{
				return HttpNotFound();
			}

			var subTaskas = GetTaskaSubTaskas(id);

			var error = false;
			foreach (SubTaska subtasca in subTaskas)
			{
				System.Diagnostics.Debug.Write("Running: " + subtasca.Command + "\n");
				Tuple<string, string> returned = RunCommanOnSever(subtasca.Command);
				string message = returned.Item1.ToString() + returned.Item2.ToString();
				subtasca.ErrorMessage = message;
				System.Diagnostics.Debug.Write(message);
				if (returned.Item2.ToString() == "" || returned.Item2.ToString().Contains("WARNING") &&
						!returned.Item2.ToString().Contains("ERROR"))
					subtasca.Status = Status.SUCCESS;
				else
				{
					subtasca.Status = Status.ERROR;
					error = true;
					break;
				}
//				Thread.Sleep(1000);
			}

			if (error)
				taska.Status = Status.ERROR;
			else
				taska.Status = Status.SUCCESS;
			db.Entry(taska).State = EntityState.Modified;
			db.SaveChanges();
			//          return View(await db.Taskas.ToListAsync());
			return RedirectToAction("Index", "Taskas");
			//          return null;
		}




		//				[HttpPost, ActionName("RunTaska")]
		//				[ValidateAntiForgeryToken]
		//				public async Task<ActionResult> RunTaska(int id)
		//				{
		//					Taska taska = await db.Taskas.FindAsync(id);
		//					taska.Status = Status.ONGOING;
		////          if (ModelState.IsValid)
		////          {
		//						db.Entry(taska).State = EntityState.Modified;
		//						await db.SaveChangesAsync();
		//						return RedirectToAction("Index");
		////          }
		////          return null; //	View(taska);
		//				}


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
