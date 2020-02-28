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
    public class PaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payment
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.DriverTbl);
            return View(payments.ToList());
        }

        // GET: Payment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentTbl paymentTbl = db.Payments.Find(id);
            if (paymentTbl == null)
            {
                return HttpNotFound();
            }
            return View(paymentTbl);
        }

        // GET: Payment/Create
        public ActionResult Create()
        {
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FirstName");
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentId,Insurance,PreHelpCost,Fine,Tax,AccidentCost,IsActive,CFDate,LFDate,CreateDate,DriverId")] PaymentTbl paymentTbl)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(paymentTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FirstName", paymentTbl.DriverId);
            return View(paymentTbl);
        }

        // GET: Payment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentTbl paymentTbl = db.Payments.Find(id);
            if (paymentTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FirstName", paymentTbl.DriverId);
            return View(paymentTbl);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentId,Insurance,PreHelpCost,Fine,Tax,AccidentCost,IsActive,CFDate,LFDate,CreateDate,DriverId")] PaymentTbl paymentTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FirstName", paymentTbl.DriverId);
            return View(paymentTbl);
        }

        // GET: Payment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentTbl paymentTbl = db.Payments.Find(id);
            if (paymentTbl == null)
            {
                return HttpNotFound();
            }
            return View(paymentTbl);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentTbl paymentTbl = db.Payments.Find(id);
            db.Payments.Remove(paymentTbl);
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
