using System.Collections.Generic;
using System.Data.Entity;
using System.Linq; 
using Sabz.DataLayer.Context;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.IService;

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
            return _drivers.Where(x=>x.IsActive).ToList();
        }

        public DriverTbl GetDriver(int? id)
        {
            return _drivers.Find(id);
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
    }
}
