using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskasWorkFlowApp.Models;

namespace TaskasWorkFlowApp.Controllers
{
    public class TaskasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Taskas
        public async Task<ActionResult> Index()
        {
            return View(await db.Taskas.ToListAsync());
        }

        // GET: Taskas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taska taska = await db.Taskas.FindAsync(id);
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
        public async Task<ActionResult> Create([Bind(Include = "Id,TaskaName,Description")] Taska taska)
        {
            if (ModelState.IsValid)
            {
                db.Taskas.Add(taska);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(taska);
        }

        // GET: Taskas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taska taska = await db.Taskas.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,TaskaName,Description")] Taska taska)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taska).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(taska);
        }

        // GET: Taskas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taska taska = await db.Taskas.FindAsync(id);
            if (taska == null)
            {
                return HttpNotFound();
            }
            return View(taska);
        }

        // POST: Taskas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Taska taska = await db.Taskas.FindAsync(id);
            db.Taskas.Remove(taska);
            await db.SaveChangesAsync();
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
