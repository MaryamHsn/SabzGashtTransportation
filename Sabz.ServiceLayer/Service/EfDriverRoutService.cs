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
    public class EfDriverRoutService : IDriverRoutService
    {
        IUnitOfWork _uow;
        readonly IDbSet<DriverRoutTbl> _driverRouts;
        public EfDriverRoutService(IUnitOfWork uow)
        {
            _uow = uow;
            _driverRouts = _uow.Set<DriverRoutTbl>();
        }

        public void AddNewDriverRout(DriverRoutTbl driverRout)
        {
            _driverRouts.Add(driverRout);
        }

        public IList<DriverRoutTbl> GetAllDriverRouts()
        {
            return _driverRouts.ToList();
        }
        public DriverRoutTbl GetDriverRout(int? id)
        {
            return _driverRouts.Find(id);
        }

        public int Delete(int id)
        {
            DriverRoutTbl driverRout = _driverRouts.Find(id);
            driverRout.IsActive = false;
            return driverRout.Id;
        }
    }
}
