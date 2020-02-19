using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Sabz.DataLayer.Context;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.IService;

namespace SabzGashtTransportation.Controllers
{
    public class LogRoutDriverController : Controller
    {
        readonly ILogRoutDriverService _logRoutDriver;
        readonly IUnitOfWork _uow;
        public LogRoutDriverController(IUnitOfWork uow, ILogRoutDriverService logRoutDriver)
        {
            _logRoutDriver = logRoutDriver;
            _uow = uow;
        }

        // GET: Automobile
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Driver = String.IsNullOrEmpty(sortOrder) ? "driver_desc" : "";
            ViewBag.Rout = sortOrder == "Rout" ? "rout_desc" : "rout";
            ViewBag.IsTemporary = sortOrder == "IsTemporary" ? "isTemporary_desc" : "isTemporary";
            ViewBag.IsDone = sortOrder == "IsDone" ? "isDone_desc" : "isDone";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var list = _logRoutDriver.GetAllLogRoutDrivers();
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.DriverRoutId.ToString().Contains(searchString)
                                       || s.IsTemporary.ToString().Contains(searchString)
                                       || s.IsDone.ToString().Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "driver_desc":
                    list = list.OrderByDescending(s => s.DriverId).ToList();
                    break;
                case "rout":
                    list = list.OrderBy(s => s.RoutId).ToList();
                    break;
                case "rout_desc":
                    list = list.OrderByDescending(s => s.RoutId).ToList();
                    break;
                case "isTemporary":
                    list = list.OrderBy(s => s.IsTemporary).ToList();
                    break;
                case "isTemporary_desc":
                    list = list.OrderByDescending(s => s.IsTemporary).ToList();
                    break;
                case "isDone":
                    list = list.OrderBy(s => s.IsDone).ToList();
                    break;
                case "isDone_desc":
                    list = list.OrderByDescending(s => s.IsDone).ToList();
                    break;
                default:
                    list = list.OrderBy(s => s.DriverId).ToList();
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));

        }

        // GET: Drivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogRoutDriverTbl routDriver= _logRoutDriver.GetLogRoutDriver(id);

            if (routDriver == null)
            {
                return HttpNotFound();
            }
            return View(routDriver);
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
        public ActionResult Create(LogRoutDriverTbl rout)
        {
            if (ModelState.IsValid)
            {
                rout.IsActive = true;
                rout.CFDate = DateTime.Now;
                rout.LFDate = DateTime.Now;

                _logRoutDriver.AddNewLogRoutDriver(rout);
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
            LogRoutDriverTbl logRoutDriver= _logRoutDriver.GetLogRoutDriver(id);

            if (logRoutDriver == null)
            {
                return HttpNotFound();
            }
            return View(logRoutDriver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LogRoutDriverTbl logRoutDriver)
        {
            if (ModelState.IsValid)
            {
                _logRoutDriver.Delete(logRoutDriver.Id);
                logRoutDriver.LFDate = DateTime.Now;
                _logRoutDriver.AddNewLogRoutDriver(logRoutDriver);
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
            LogRoutDriverTbl logRoutDriver= _logRoutDriver.GetLogRoutDriver(id);
            if (logRoutDriver == null)
            {
                return HttpNotFound();
            }
            return View(logRoutDriver);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _logRoutDriver.Delete(id);
            _uow.SaveAllChanges();
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
