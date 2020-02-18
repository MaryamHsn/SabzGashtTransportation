using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sabz.DataLayer.Context;
using Sabz.DomainClasses.DTO;

namespace SabzGashtTransportation.Controllers
{
    public class AccidentTblsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccidentTbls
        public ActionResult Index()
        {
            var accidents = db.Accidents.Include(a => a.DriverTbl);
            return View(accidents.ToList());
        }

        // GET: AccidentTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccidentTbl accidentTbl = db.Accidents.Find(id);
            if (accidentTbl == null)
            {
                return HttpNotFound();
            }
            return View(accidentTbl);
        }

        // GET: AccidentTbls/Create
        public ActionResult Create()
        {
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FirstName");
            return View();
        }

        // POST: AccidentTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccidentId,UseInsurence,Cost,Description,IsActive,CFDate,LFDate,DriverId,AutomobileId")] AccidentTbl accidentTbl)
        {
            if (ModelState.IsValid)
            {
                db.Accidents.Add(accidentTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FirstName", accidentTbl.DriverId);
            return View(accidentTbl);
        }

        // GET: AccidentTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccidentTbl accidentTbl = db.Accidents.Find(id);
            if (accidentTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FirstName", accidentTbl.DriverId);
            return View(accidentTbl);
        }

        // POST: AccidentTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccidentId,UseInsurence,Cost,Description,IsActive,CFDate,LFDate,DriverId,AutomobileId")] AccidentTbl accidentTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accidentTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FirstName", accidentTbl.DriverId);
            return View(accidentTbl);
        }

        // GET: AccidentTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccidentTbl accidentTbl = db.Accidents.Find(id);
            if (accidentTbl == null)
            {
                return HttpNotFound();
            }
            return View(accidentTbl);
        }

        // POST: AccidentTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccidentTbl accidentTbl = db.Accidents.Find(id);
            db.Accidents.Remove(accidentTbl);
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
