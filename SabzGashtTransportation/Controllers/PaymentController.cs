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
using Sabz.ServiceLayer.Mapper;
using Sabz.ServiceLayer.Utils;
using SabzGashtTransportation.ViewModel;

namespace SabzGashtTransportation.Controllers
{
    public class PaymentController : Controller
    {
        readonly IPaymentService _payment;
        private readonly IDriverService _driver;
        private PaymentViewModel common { get; set; }
        private List<PaymentViewModel> commonList { get; set; }
        readonly IUnitOfWork _uow;
        public PaymentController(IUnitOfWork uow, IPaymentService payment, IDriverService driver)
        {
            _driver = driver;
            _payment = payment;
            _uow = uow;
        }

        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            commonList = new List<PaymentViewModel>();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Driver = String.IsNullOrEmpty(sortOrder) ? "driver_desc" : "";
            ViewBag.Insurance = sortOrder == "insurance" ? "insurance_desc" : "insurance";
            ViewBag.PreHelpCost = sortOrder == "preHelpCost" ? "preHelpCost_desc" : "preHelpCost";
            ViewBag.Fine = sortOrder == "fine" ? "fine_desc" : "fine";
            ViewBag.Tax = sortOrder == "tax" ? "tax_desc" : "tax";
            ViewBag.AccidentCost = sortOrder == "accidentCost" ? "accidentCost_desc" : "accidentCost";
            ViewBag.CreateDate = sortOrder == "createDate" ? "createDate_desc" : "createDate";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var payment = _payment.GetAllPayments().ToList();
            var driver = _driver.GetAllDrivers().ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                payment = payment.Where(s => s.DriverTbl.FirstName.Contains(searchString)
                                               || s.DriverTbl.LastName.Contains(searchString)
                                               || s.Insurance.ToString().Contains(searchString)
                                               || s.PreHelpCost.ToString().Contains(searchString)
                                               || s.Tax.ToString().Contains(searchString)
                                               || s.AccidentCost.ToString().Contains(searchString)
                                               || s.CreateDate.ToPersianDateString().Contains(searchString)
                                               || s.Fine.ToString().Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "driver_desc":
                    //var driver = _driver.GetAllDrivers().OrderByDescending(s => s.DriverId).ToList();
                    payment = payment.OrderByDescending(s => s.DriverTbl.FullName).ToList();
                    break;
                case "insurance":
                    payment = payment.OrderBy(s => s.Insurance).ToList();
                    break;
                case "insurance_desc":
                    payment = payment.OrderByDescending(s => s.Insurance).ToList();
                    break;
                case "preHelpCost":
                    payment = payment.OrderBy(s => s.PreHelpCost).ToList();
                    break;
                case "preHelpCost_desc":
                    payment = payment.OrderByDescending(s => s.PreHelpCost).ToList();
                    break;
                case "fine":
                    payment = payment.OrderBy(s => s.Fine).ToList();
                    break;
                case "fine_desc":
                    payment = payment.OrderByDescending(s => s.Fine).ToList();
                    break;
                case "tax":
                    payment = payment.OrderBy(s => s.Tax).ToList();
                    break;
                case "tax_desc":
                    payment = payment.OrderByDescending(s => s.Tax).ToList();
                    break;
                case "accidentCost":
                    payment = payment.OrderBy(s => s.AccidentCost).ToList();
                    break;
                case "accidentCost_desc":
                    payment = payment.OrderByDescending(s => s.AccidentCost).ToList();
                    break;
                case "createDate":
                    payment = payment.OrderBy(s => s.CreateDate).ToList();
                    break;
                case "createDate_desc":
                    payment = payment.OrderByDescending(s => s.CreateDate).ToList();
                    break;
                default:
                    payment = payment.OrderBy(s => s.DriverId).ToList();
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            foreach (var item in payment)
            {
                var element = BaseMapper<PaymentViewModel, PaymentTbl>.Map(item);
                element.DriverFirstName = driver.Where(x => x.DriverId == item.DriverId).SingleOrDefault() != null ? driver.Where(x => x.DriverId == item.DriverId).SingleOrDefault().FirstName : "--";
                element.DriverLastName = driver.Where(x => x.DriverId == item.DriverId).SingleOrDefault() != null ? driver.Where(x => x.DriverId == item.DriverId).SingleOrDefault().LastName : "--";
                element.DriverFullName = element.DriverFirstName + " " + element.DriverLastName;
                element.CreateDateString = item.CreateDate.ToPersianDateString();
                commonList.Add(element);
            }
            return View(commonList.ToPagedList(pageNumber, pageSize));
        }

        // GET: Drivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            common = new PaymentViewModel();
            var payment = _payment.GetPayment(id);
            common = BaseMapper<PaymentViewModel, PaymentTbl>.Map(payment);
            common.DriverFullName = payment.DriverTbl != null
                ? payment.DriverTbl.FullName : _driver.GetDriver(payment.DriverId).FullName;
            common.CFDateString = payment.CFDate.ToPersianDateString();
            common.LFDateString = payment.LFDate.ToPersianDateString();
            common.CreateDateString = payment.CreateDate.ToPersianDateString();

            if (common == null)
            {
                return HttpNotFound();
            }
            return View(common);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            common = new PaymentViewModel()
            {
                Drivers = _driver.GetAllDrivers(),
            };
            return View(common);
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentViewModel payment)
        {
            if (ModelState.IsValid)
            {
                var obj = BaseMapper<PaymentTbl, PaymentViewModel>.Map(payment);
                obj.IsActive = true;
                obj.CFDate = DateTime.Now;
                obj.LFDate = DateTime.Now;
                obj.CreateDate = payment.CreateDateString.ToGeorgianDate();
                _payment.AddNewPayment(obj);
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
            PaymentTbl payment = _payment.GetPayment(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            var element = BaseMapper<PaymentViewModel, PaymentTbl>.Map(payment);
            element.DriverFirstName = payment.DriverTbl != null ? payment.DriverTbl.FirstName : "--";
            element.DriverLastName = payment.DriverTbl != null ? payment.DriverTbl.LastName : "--";
            element.DriverFullName = element.DriverFirstName + " " + element.DriverLastName;
            element.CreateDateString = payment.CreateDate.ToPersianDateString();
            element.Drivers = _driver.GetAllDrivers().ToList();
            return View(element);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaymentViewModel payment)
        {
            if (ModelState.IsValid)
            {
                payment.LFDate = DateTime.Now;
                payment.IsActive = false;
                _payment.Delete(payment.PaymentId);
                var obj = BaseMapper<PaymentTbl, PaymentViewModel>.Map(payment);
                obj.CFDate = DateTime.Now;
                obj.LFDate = DateTime.Now;
                obj.IsActive = true;
                obj.CreateDate = payment.CreateDateString.ToGeorgianDate();
                _payment.AddNewPayment(obj);
                _uow.SaveAllChanges();
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
            PaymentTbl payment = _payment.GetPayment(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            var obj = BaseMapper<PaymentTbl, PaymentViewModel>.Map(payment);
            obj.DriverFullName = payment.DriverTbl != null ? payment.DriverTbl.FullName : _driver.GetDriver(payment.DriverId).FullName;
            obj.CreateDateString = payment.CreateDate.ToPersianDateString();
            return View(obj);

        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _payment.Delete(id);
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
