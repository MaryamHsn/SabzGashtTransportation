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
    public class RoutController : Controller
    {
        readonly IRoutService _rout;
        readonly IUnitOfWork _uow;
        public RoutController(IUnitOfWork uow, IRoutService rout)
        {
            _rout = rout;
            _uow = uow;
        }

        // GET: Automobile
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ShiftType = String.IsNullOrEmpty(sortOrder) ? "shiftType_desc" : "";
            ViewBag.EnterTime = sortOrder == "EnterTime" ? "enterTime_desc" : "enterTime";
            ViewBag.ExitTime = sortOrder == "ExitTime" ? "exitTime_desc" : "exitTime";
            ViewBag.RegionId = sortOrder == "Region" ? "region_desc" : "region";
            ViewBag.AutomobileType = sortOrder == "AutomobileType" ? "automobileType_desc" : "AutomobileType";
            ViewBag.Count = sortOrder == "Count" ? "count_desc" : "count";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var list = _rout.GetAllRouts();
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.ShiftType.ToString().Contains(searchString)
                                       || s.EnterTime.ToString().Contains(searchString)
                                       || s.ExitTime.ToString().Contains(searchString)
                                       || s.RegionId.ToString().Contains(searchString)
                                       || s.AutomobileTypeId.ToString().Contains(searchString)
                                       || s.Count.ToString().Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "shiftType_desc":
                    list = list.OrderByDescending(s => s.ShiftType).ToList();
                    break;
                case "enterTime":
                    list = list.OrderBy(s => s.EnterTime).ToList();
                    break;
                case "enterTime_desc":
                    list = list.OrderByDescending(s => s.EnterTime).ToList();
                    break;
                case "exitTime":
                    list = list.OrderBy(s => s.AutomobileTypeId).ToList();
                    break;
                case "exitTime_desc":
                    list = list.OrderByDescending(s => s.AutomobileTypeId).ToList();
                    break;
                case "region":
                    list = list.OrderBy(s => s.RegionId).ToList();
                    break;
                case "region_desc":
                    list = list.OrderByDescending(s => s.RegionId).ToList();
                    break;
                case "automobileType":
                    list = list.OrderBy(s => s.AutomobileTypeId).ToList();
                    break;
                case "automobileType_desc":
                    list = list.OrderByDescending(s => s.AutomobileTypeId).ToList();
                    break;
                case "count":
                    list = list.OrderBy(s => s.Count).ToList();
                    break;
                case "count_desc":
                    list = list.OrderByDescending(s => s.Count).ToList();
                    break;
                default:
                    list = list.OrderBy(s => s.ShiftType).ToList();
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
            RoutTbl rout= _rout.GetRout(id);

            if (rout == null)
            {
                return HttpNotFound();
            }
            return View(rout);
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
        public ActionResult Create(RoutTbl rout)
        {
            if (ModelState.IsValid)
            {
                rout.IsActive = true;
                rout.CFDate = DateTime.Now;
                rout.LFDate = DateTime.Now;

                _rout.AddNewRout(rout);
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
            RoutTbl rout= _rout.GetRout(id);

            if (rout == null)
            {
                return HttpNotFound();
            }
            return View(rout);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoutTbl rout)
        {
            if (ModelState.IsValid)
            {
                _rout.Delete(rout.RoutID);
                rout.LFDate = DateTime.Now;
                _rout.AddNewRout(rout);
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
            RoutTbl rout = _rout.GetRout(id);
            if (rout == null)
            {
                return HttpNotFound();
            }
            return View(rout);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _rout.Delete(id);
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
