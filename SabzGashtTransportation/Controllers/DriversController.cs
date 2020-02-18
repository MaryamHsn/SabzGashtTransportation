﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sabz.DataLayer.Context;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.IService;

namespace SabzGashtTransportation.Controllers
{
    public class DriversController : Controller
    {
        readonly IDriverService _drivers;
        readonly IUnitOfWork _uow;
        public DriversController(IUnitOfWork uow, IDriverService drivers)
        {
            _drivers = drivers;
            _uow = uow;
        }

       // private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Drivers
        [HttpGet]
        public ActionResult Index()
        {
            //return View(db.Drivers.ToList());
            var list = _drivers.GetAllDrivers();
            return View(list);
        } 

        // GET: Drivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // DriverTbl driverTbl = db.Drivers.Find(id); 
            DriverTbl driverTbl = _drivers.GetDriver(id);

            if (driverTbl == null)
            {
                return HttpNotFound();
            }
            return View(driverTbl);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( DriverTbl driver)
        {
            if (ModelState.IsValid)
            {
                //db.Drivers.Add(driverTbl);
                // db.SaveChanges();
                _drivers.AddNewDriver(driver);
                _uow.SaveAllChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Drivers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DriverTbl driverTbl = db.Drivers.Find(id);
            DriverTbl driverTbl =_drivers.GetDriver(id);

            if (driverTbl == null)
            {
                return HttpNotFound();
            }
            return View(driverTbl);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( DriverTbl driver)
        {
            if (ModelState.IsValid)
            {
                int id =_drivers.Delete(driver.DriverId);
                _drivers.AddNewDriver(driver);
                _uow.SaveAllChanges();

                // db.Entry(driverTbl).State = EntityState.Modified;
                //   db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        // GET: Drivers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverTbl driverTbl = _drivers.GetDriver(id);

            // DriverTbl driverTbl = db.Drivers.Find(id);
            if (driverTbl == null)
            {
                return HttpNotFound();
            }
            return View(driverTbl);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _drivers.Delete(id);
            _uow.SaveAllChanges();

            //DriverTbl driverTbl = db.Drivers.Find(id);
            //db.Drivers.Remove(driverTbl);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}