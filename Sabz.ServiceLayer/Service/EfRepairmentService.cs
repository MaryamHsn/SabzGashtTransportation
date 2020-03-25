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
        public EfRepairmentService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void AddNewRepairment(RepairmentTbl repairment)
        {
            _uow.RepairmentRepository.Add(repairment);
            _uow.SaveAllChanges();
        }

        public IList<RepairmentTbl> GetAllRepairment()
        {
            return _uow.RepairmentRepository.GetAll().ToList();
        }

        public RepairmentTbl GetRepairment(int? id)
        {
            return _uow.RepairmentRepository.Get((int)id);
        }

        public bool Delete(int id)
        {
            RepairmentTbl repairment = _uow.RepairmentRepository.Get(id); 
            var t = _uow.RepairmentRepository.SoftDelete(repairment);
            _uow.SaveAllChanges();
            return t;
        }
    }

}
