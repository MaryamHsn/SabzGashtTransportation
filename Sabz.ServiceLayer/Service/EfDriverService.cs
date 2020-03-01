using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq; 
using Sabz.DataLayer.Context;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.IService;
using Sabz.ServiceLayer.Mapper; 

namespace Sabz.ServiceLayer.Service
{
    public class EfDriverService : IDriverService
    {
        IUnitOfWork _uow;
        readonly IDbSet<DriverTbl> _drivers;
        public EfDriverService(IUnitOfWork uow)
        {
            _uow = uow;
            _drivers = _uow.Set<DriverTbl>();
        }

        public IList<DriverTbl> GetAllDrivers()
        {
            return _drivers.Where(x => x.IsActive).ToList();
        }

        public DriverTbl GetDriver(int? id)
        {
            return _drivers.Where(x => x.IsActive && x.DriverId==id).FirstOrDefault();
        }

        public DriverTbl GetDriverByName(string fullName)
        {
            return _drivers.Where(x => x.IsActive && x.FullName.Contains(fullName)).FirstOrDefault();
        }

        public void AddNewDriver(DriverTbl driver)
        {
             _drivers.Add(driver); 
        }

        public int Delete(int id)
        {
            DriverTbl driver = _drivers.Find(id);
            driver.IsActive = false;
            return driver.DriverId;
        }
        //public List<Driver> GetAllDrivers()
        //{
        //    var entities = _drivers.Where(x => x.IsActive).ToList(); 
        //    var t=entities.Select(BaseMapper<DriverTbl, Driver>.Map).ToList();
        //    return t;
        //}

        //public Driver GetDriver(int? id)
        //{
        //    var entity = _drivers.Find(id);
        //    var t= BaseMapper<DriverTbl, Driver>.Map(entity);
        //    return t;
        //}

        //public void AddNewDriver(Driver driver)
        //{
        //    // _drivers.Add(driver); 
        //    var entity = BaseMapper<Sabz.ServiceLayer.ViewModel.Driver, DriverTbl>.Map(driver);
        //    _drivers.Add(entity);
        //}

        //public int Delete(int id)
        //{
        //    DriverTbl driver = _drivers.Find(id);
        //    driver.IsActive = false;
        //    return driver.DriverId;
        //}
    }
}
