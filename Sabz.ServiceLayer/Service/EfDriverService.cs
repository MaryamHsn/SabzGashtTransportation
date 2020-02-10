using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void AddNewDriver(DriverTbl driverRout)
        {
            _drivers.Add(driverRout);
        }

        public IList<DriverTbl> GetAllDrivers()
        {
            return _drivers.ToList();
        }
    }
}
