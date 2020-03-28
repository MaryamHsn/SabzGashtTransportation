using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sabz.DataLayer.Context;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.IService;

namespace Sabz.ServiceLayer.Service
{
    public class EfAutomobileTypeService : IAutomobileTypeService
    {
        IUnitOfWork _uow;
        public EfAutomobileTypeService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void AddNewAutomobileType(AutomobileTypeTbl automobileType)
        {
            _uow.AutomobileTypeRepository.Add(automobileType);
        }
        public IList<AutomobileTypeTbl> GetAllAutomobileTypes()
        {
            return _uow.AutomobileTypeRepository.GetAll().ToList();
        }
        public AutomobileTypeTbl GetAutomobileType(int? id)
        {
            return _uow.AutomobileTypeRepository.Get((int)id);
        }
        public AutomobileTypeTbl GetAutomobileTypeByCoolerBus(int cooler,int bus)
        {
            var t = Convert.ToBoolean(cooler);
            var autoType= _uow.AutomobileTypeRepository.GetAll(x => x.IsActive && x.HasCooler == t &&x.IsBus==bus).FirstOrDefault();
            return autoType;
        }
        public bool Delete(int id)
        {
            AutomobileTypeTbl automobileType = _uow.AutomobileTypeRepository.Get(id);
            var t = _uow.AutomobileTypeRepository.SoftDelete(automobileType);
            _uow.SaveAllChanges();
            return t;
        }
        public void UpdateAutomobileType(AutomobileTypeTbl automobileType)
        {
            _uow.AutomobileTypeRepository.Update(automobileType);
        }

        ////Async 
        public async Task AddNewAutomobileTypeAsync(AutomobileTypeTbl AutomobileType, CancellationToken ct = new CancellationToken())
        {
            await _uow.AutomobileTypeRepository.AddAsync(AutomobileType, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<AutomobileTypeTbl>> GetAllAutomobileTypesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.AutomobileTypeRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<AutomobileTypeTbl> GetAutomobileTypeAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.AutomobileTypeRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<AutomobileTypeTbl> GetAutomobileTypeByCoolerBus(int cooler, int bus, CancellationToken ct = new CancellationToken())
        {
            var t = Convert.ToBoolean(cooler);
            var autoType =await  _uow.AutomobileTypeRepository.GetAllAsync(x => x.IsActive && x.HasCooler == t && x.IsBus == bus);
            return autoType.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var AutomobileType = await _uow.AutomobileTypeRepository.GetAsync(id, ct);
            var obj = await _uow.AutomobileTypeRepository.SoftDeleteAsync(AutomobileType);
            _uow.SaveAllChanges();
            return obj;
        }
        public async Task UpdateAutomobileTypeAsync(AutomobileTypeTbl automobileType)
        {
            await _uow.AutomobileTypeRepository.UpdateAsync(automobileType);
        }
    }
}
