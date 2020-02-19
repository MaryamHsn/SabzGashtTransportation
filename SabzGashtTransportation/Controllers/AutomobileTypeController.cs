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
    public class AutomobileTypeController : Controller
    {
        readonly IAutomobileTypeService _automobile;
        readonly IUnitOfWork _uow;
        public AutomobileTypeController(IUnitOfWork uow, IAutomobileTypeService automobile)
        {
            _automobile = automobile;
            _uow = uow;
        }

        // GET: Automobile
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.HasCooler = String.IsNullOrEmpty(sortOrder) ? "hasCooler_desc" : "";
            ViewBag.IsBus = sortOrder == "IsBus" ? "isBus_desc" : "isBus";
           
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var list = _automobile.GetAllAutomobileTypes();
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.HasCooler.ToString().Contains(searchString)
                                       || s.IsBus.ToString().Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "hasCooler_desc":
                    list = list.OrderByDescending(s => s.HasCooler).ToList();
                    break;
                case "isBus":
                    list = list.OrderBy(s => s.IsBus).ToList();
                    break;
                case "isBus_desc":
                    list = list.OrderByDescending(s => s.IsBus).ToList();
                    break; 
                default:
                    list = list.OrderBy(s => s.HasCooler).ToList();
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
            AutomobileTypeTbl automobile = _automobile.GetAutomobileType(id);

            if (automobile == null)
            {
                return HttpNotFound();
            }
            return View(automobile);
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
        public ActionResult Create(AutomobileTypeTbl automobile)
        {
            if (ModelState.IsValid)
            {
                automobile.IsActive = true;
                automobile.CFDate = DateTime.Now;
                automobile.LFDate = DateTime.Now;

                _automobile.AddNewAutomobileType(automobile);
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
            AutomobileTypeTbl automobile = _automobile.GetAutomobileType(id);

            if (automobile == null)
            {
                return HttpNotFound();
            }
            return View(automobile);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AutomobileTypeTbl automobile)
        {
            if (ModelState.IsValid)
            {
                _automobile.Delete(automobile.AutoTypeId);
                automobile.LFDate = DateTime.Now;
                _automobile.AddNewAutomobileType(automobile);
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
            AutomobileTypeTbl automobile = _automobile.GetAutomobileType(id);
            if (automobile == null)
            {
                return HttpNotFound();
            }
            return View(automobile);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _automobile.Delete(id);
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
