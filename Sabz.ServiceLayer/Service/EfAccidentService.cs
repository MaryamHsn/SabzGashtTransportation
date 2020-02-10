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
    public class EfAccidentService : IAccidentService
    {
        IUnitOfWork _uow;
        readonly IDbSet<AccidentTbl> _accidents;
        public EfAccidentService(IUnitOfWork uow)
        {
            _uow = uow;
            _accidents = _uow.Set<AccidentTbl>();
        }

        public void AddNewAccident(AccidentTbl accident)
        {
            _accidents.Add(accident);
        }

        public IList<AccidentTbl> GetAllAccidents()
        {
            return _accidents.ToList();
        }
    }
}
