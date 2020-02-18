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
    public class EfAutomobileTypeService : IAutomobileTypeService
    {
        IUnitOfWork _uow;
        readonly IDbSet<AutomobileTypeTbl> _automobileTypes;
        public EfAutomobileTypeService(IUnitOfWork uow)
        {
             _uow = uow;
             _automobileTypes = _uow.Set<AutomobileTypeTbl>();
        }

        public void AddNewAutomobileType(AutomobileTypeTbl automobileType)
        {
            _automobileTypes.Add(automobileType);
        }

        public IList<AutomobileTypeTbl> GetAllAutomobileTypes()
        {
            return _automobileTypes.Where(x => x.IsActive).ToList();
        }
        public AutomobileTypeTbl GetAutomobileType(int? id)
        {
            return _automobileTypes.Find(id);
        }

        public int Delete(int id)
        {
            AutomobileTypeTbl automobileType = _automobileTypes.Find(id);
            automobileType.IsActive = false;
            return automobileType.AutoTypeId;
        }
    }
}
