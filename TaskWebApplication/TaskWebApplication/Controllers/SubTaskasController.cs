﻿using System;
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
    public class SubTaskasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubTaskas
				public ActionResult Index(int? id)
        {
					if (id != null)
					{
						var subTaskas = db.SubTaskas.Include(s => s.ParentTaska)
							.Where(item => item.TaskaId == id)
							.OrderBy(x => x.TaskaId)
							.ThenBy(x => x.Id);
						return View(subTaskas.ToList());
					}
					else
					{
						var subTaskas = db.SubTaskas.Include(s => s.ParentTaska)
							.OrderBy(x => x.TaskaId)
							.ThenBy(x => x.Id); ;
						return View(subTaskas.ToList());
					}
        }

        // GET: SubTaskas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubTaska subTaska = db.SubTaskas.Find(id);
            if (subTaska == null)
            {
                return HttpNotFound();
            }
            return View(subTaska);
        }

        // GET: SubTaskas/Create
        public ActionResult Create()
        {
            ViewBag.TaskaId = new SelectList(db.Taskas, "Id", "Description");
            return View();
        }

        // POST: SubTaskas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Command,ErrorMessage,Status,TaskaId")] SubTaska subTaska)
        {
            if (ModelState.IsValid)
            {
                db.SubTaskas.Add(subTaska);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TaskaId = new SelectList(db.Taskas, "Id", "Description", subTaska.TaskaId);
            return View(subTaska);
        }

        // GET: SubTaskas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubTaska subTaska = db.SubTaskas.Find(id);
            if (subTaska == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaskaId = new SelectList(db.Taskas, "Id", "Description", subTaska.TaskaId);
            return View(subTaska);
        }

        // POST: SubTaskas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Command,ErrorMessage,Status,TaskaId")] SubTaska subTaska)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subTaska).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TaskaId = new SelectList(db.Taskas, "Id", "Description", subTaska.TaskaId);
            return View(subTaska);
        }

        // GET: SubTaskas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubTaska subTaska = db.SubTaskas.Find(id);
            if (subTaska == null)
            {
                return HttpNotFound();
            }
            return View(subTaska);
        }

        // POST: SubTaskas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubTaska subTaska = db.SubTaskas.Find(id);
            db.SubTaskas.Remove(subTaska);
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
