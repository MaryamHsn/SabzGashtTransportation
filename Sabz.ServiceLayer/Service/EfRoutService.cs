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
    public class EfRoutService : IRoutService
    {
        IUnitOfWork _uow;
        readonly IDbSet<RoutTbl> _routs;
        public EfRoutService(IUnitOfWork uow)
        {
            _uow = uow;
            _routs = _uow.Set<RoutTbl>();
        }

        public void AddNewRout(RoutTbl rout)
        {
            _routs.Add(rout);
        }

        public IList<RoutTbl> GetAllRouts()
        {
            return _routs.ToList();
        }
    }
}
