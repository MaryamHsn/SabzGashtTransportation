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
    public class EfRepairmentService : IRepairmentService
    {
        IUnitOfWork _uow;
        readonly IDbSet<RepairmentTbl> _repairments;
        public EfRepairmentService(IUnitOfWork uow)
        {
            _uow = uow;
            _repairments = _uow.Set<RepairmentTbl>();
        }

        public void AddNewRepairment(RepairmentTbl repairment)
        {
            _repairments.Add(repairment);
        }

        public IList<RepairmentTbl> GetAllRepairment()
        {
            return _repairments.ToList();
        }
    }

}
