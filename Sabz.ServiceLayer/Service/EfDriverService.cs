using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq; 
using Sabz.DataLayer.Context;
using Sabz.DataLayer.IRepository;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.IService;
using Sabz.ServiceLayer.Mapper; 

namespace Sabz.ServiceLayer.Service
{
    public class EfDriverService : IDriverService
    {
        IUnitOfWork _uow;
       // readonly IDrivers _drivers;
       // readonly IDbSet<DriverTbl> _drivers;
        DateTime _now;
        public EfDriverService(IUnitOfWork uow)//, IDrivers drivers
        {
            _now = DateTime.Now;
            _uow = uow;
            //_drivers = _uow.Set<DriverTbl>();
            //_drivers = drivers;
        }

        public IList<DriverTbl> GetAllDrivers()
        {
            return _uow.Driver.Get(x => x.IsActive).ToList();
        }
        public IList<DriverTbl> GetAllDriversByIds(List<int> ids)
        {
            return _uow.Driver.Get(x =>ids.Contains(x.DriverId) && x.IsActive).ToList();
        }
        public IList<DriverTbl> GetOtherDriversByIds(List<int> ids)
        {
            return _uow.Driver.Get(x => !ids.Contains(x.DriverId) && x.IsActive).ToList();
        }
        public DriverTbl GetDriver(int? id)
        {
            return _uow.Driver.Get(x => x.IsActive && x.DriverId==id).FirstOrDefault();
        }

        public DriverTbl GetDriverByName(string fullName)
        {
            return _uow.Driver.Get(x => x.IsActive && x.FullName.Contains(fullName)).FirstOrDefault();
        }

        public void AddNewDriver(DriverTbl driver)
        {
            _uow.Driver.Add(driver);
            _uow.SaveAllChanges();

        }

        public int Delete(int id)
        {
            DriverTbl driver = _uow.Driver.Get(id);

            _uow.Driver.SoftDelete(driver);
            driver.IsActive = false;
            _uow.SaveAllChanges();

            return driver.DriverId;
        }

        public DriverTbl UpdateDriver(DriverTbl entity)
        {
            var old = _uow.Driver.Get(entity.DriverId);
            
            entity.CreatedDate = old.CreatedDate;
            entity.ModifiedDate = _now;
            entity = _uow.Driver.Update(entity);
            _uow.SaveAllChanges();

            return entity;
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
