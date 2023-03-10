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
using Sabz.ServiceLayer.Enumration;
using Sabz.ServiceLayer.IService;
using Sabz.ServiceLayer.Mapper;
using Sabz.ServiceLayer.Utils;
using SabzGashtTransportation.ViewModel;

namespace SabzGashtTransportation.Controllers
{
    [Authorize]
    public class AutomobileTypeController : Controller
    {
        readonly IAutomobileTypeService _automobile;
        readonly IUnitOfWork _uow;
        private AutomobileTypeViewModel common { get; set; }
        private List<AutomobileTypeViewModel> commonList { get; set; }

        public AutomobileTypeController(IUnitOfWork uow, IAutomobileTypeService automobile)
        {
            _automobile = automobile;
            _uow = uow;
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        commonList = new List<AutomobileTypeViewModel>();
                        ViewBag.CurrentSort = sortOrder;
                        ViewBag.HasCooler = String.IsNullOrEmpty(sortOrder) ? "hasCooler_desc" : "";
                        ViewBag.IsBus = sortOrder == "isBus" ? "isBus_desc" : "isBus";

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

                        int pageSize = 10;
                        int pageNumber = (page ?? 1);
                        foreach (var item in list)
                        {
                            var element = BaseMapper<AutomobileTypeViewModel, AutomobileTypeTbl>.Map(item);
                            commonList.Add(element);
                        }
                        return View(commonList.ToPagedList(pageNumber, pageSize));
                    }
                }
                return RedirectToAction("login", "Account");
            }
            catch (Exception)
            {
                return View();
            }
        }

        //[Authorize(Roles = "admin , SuperViser")]
        public ActionResult Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
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
                    common = new AutomobileTypeViewModel();
                    common = BaseMapper<AutomobileTypeViewModel, AutomobileTypeTbl>.Map(automobile);
                    //common.CreatedDateString = automobile.CreatedDate.ToPersianDateString();
                    //common.ModifiedDateString = ((DateTime)automobile.ModifiedDate).ToPersianDateString();
                    return View(common);
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return View();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[Authorize(Roles = "admin , SuperViser")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AutomobileTypeViewModel automobile)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        if (ModelState.IsValid)
                        {
                            var obj = BaseMapper<AutomobileTypeViewModel, AutomobileTypeTbl>.Map(automobile);
                            //obj.IsActive = true;
                            //obj.CreatedDate = DateTime.Now;
                            //obj.ModifiedDate = DateTime.Now;
                            obj.HasCooler = Convert.ToBoolean(automobile.HasCoolerEnum);
                            obj.IsBus = (int)automobile.IsBusEnum;
                            _automobile.AddNewAutomobileType(obj);
                            _uow.SaveAllChanges();
                            return RedirectToAction("Index");
                        }
                        return View();
                    }
                }
                return RedirectToAction("login", "Account");
            }
            catch (Exception)
            {
                return View();
            }
        }

        //[Authorize(Roles = "admin , SuperViser")]
        public ActionResult Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
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
                    var obj = BaseMapper<AutomobileTypeViewModel, AutomobileTypeTbl>.Map(automobile);
                    //  obj.HasCoolerEnum = (HasCoolerEnum) Enum.Parse(typeof(HasCoolerEnum), obj.HasCooler.ToString(), true);
                    if (obj.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler))
                    {
                        obj.HasCoolerEnum = HasCoolerEnum.HasCooler;
                    }
                    else
                    {
                        obj.HasCoolerEnum = HasCoolerEnum.HasNotCooler;
                    }
                    if (obj.IsBus == (int)AutomobileTypeEnum.Bus)
                    {
                        obj.IsBusEnum = AutomobileTypeEnum.Bus;
                    }
                    else
                    {
                        obj.IsBusEnum = AutomobileTypeEnum.MiniBus;
                    }


                    return View(obj);
                }
            }
            return RedirectToAction("login", "Account");
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[Authorize(Roles = "admin , SuperViser")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AutomobileTypeViewModel automobile)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        automobile.HasCooler = Convert.ToBoolean(automobile.HasCoolerEnum);
                        automobile.IsBus = (int)automobile.IsBusEnum;
                        automobile.IsActive = true;
                        var obj = BaseMapper<AutomobileTypeTbl, AutomobileTypeViewModel>.Map(automobile);
                        _automobile.UpdateAutomobileType(obj);
                        _uow.SaveAllChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        public ActionResult Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
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
                    var obj = BaseMapper<AutomobileTypeTbl, AutomobileTypeViewModel>.Map(automobile);
                    if (obj.IsBus == (int)AutomobileTypeEnum.Bus)
                    {
                        ViewBag.AutomobileType = AutomobileTypeEnum.Bus;
                    }
                    else
                    {
                        ViewBag.AutomobileType = AutomobileTypeEnum.MiniBus;
                    }

                    return View(obj);
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    _automobile.Delete(id);
                    _uow.SaveAllChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("login", "Account");
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
