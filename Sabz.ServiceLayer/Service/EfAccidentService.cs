using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabz.DataLayer.Context;
using Sabz.DataLayer.IRepository;
using Sabz.DataLayer.Repository;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.IService;

namespace Sabz.ServiceLayer.Service
{ 
    public class EfAccidentService :  IAccidentService
    {
        //IUnitOfWork _uow;
        //readonly IDbSet<AccidentTbl> _accidents;
        readonly IAccidentRepository _accidents;
        public EfAccidentService(IAccidentRepository accidents)
        {
            _accidents = accidents;
        }
        //public EfAccidentService(IUnitOfWork uow)
        //{
        //    _uow = uow;
        //    _accidents = _uow.Set<AccidentTbl>();
        //}
        
        public void AddNewAccident(AccidentTbl accident)
        {
            _accidents.Add(accident);
        }

        public IList<AccidentTbl> GetAllAccidents()
        {
            return _accidents.Get(x=>x.IsActive).ToList();
        }
        public AccidentTbl GetAccident(int? id)
        {
            return _accidents.Get(x=>x.IsActive&&x.AccidentId==id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            var entity = _accidents.Get(id);
            entity.IsActive = false;

            var accident = _accidents.Delete(entity);
            return accident;
        }
    }
}
