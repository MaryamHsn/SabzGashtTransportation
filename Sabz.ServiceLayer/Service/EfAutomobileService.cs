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
    public class EfAutomobileService : IAutomobileService
    {
        IUnitOfWork _uow;
        readonly IDbSet<AutomobileTbl> _automobiles;
        public EfAutomobileService(IUnitOfWork uow)
        {
            _uow = uow;
            _automobiles = _uow.Set<AutomobileTbl>();
        }

        public void AddNewAutomobile(AutomobileTbl automobile)
        {
            _automobiles.Add(automobile);
        }

        public IList<AutomobileTbl> GetAllAutomobiles()
        {
            return _automobiles.ToList();
        }
    }
}
