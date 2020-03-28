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
    public class EfAutomobileService : IAutomobileService
    {
        IUnitOfWork _uow;
        public EfAutomobileService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void AddNewAutomobile(AutomobileTbl automobile)
        {
            _uow.AutomobileRepository.Add(automobile);
        }

        public IList<AutomobileTbl> GetAllAutomobiles()
        {
            return _uow.AutomobileRepository.GetAll().ToList();
        }
        public AutomobileTbl GetAutomobile(int? id)
        {
            return _uow.AutomobileRepository.Get((int)id);
        }

        public bool Delete(int id)
        {
            AutomobileTbl automobile = _uow.AutomobileRepository.Get(id);
            var t = _uow.AutomobileRepository.SoftDelete(automobile);
            _uow.SaveAllChanges();
            return t;
        }
        public void UpdateAutomobile(AutomobileTbl automobile)
        {
            _uow.AutomobileRepository.Update(automobile);
        }

        ////Async 
        public async Task AddNewAutomobileAsync(AutomobileTbl Automobile, CancellationToken ct = new CancellationToken())
        {
            await _uow.AutomobileRepository.AddAsync(Automobile, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<AutomobileTbl>> GetAllAutomobilesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.AutomobileRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<AutomobileTbl> GetAutomobileAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.AutomobileRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Automobile = await _uow.AutomobileRepository.GetAsync(id, ct);
            var obj = await _uow.AutomobileRepository.SoftDeleteAsync(Automobile);
            _uow.SaveAllChanges();
            return obj;
        }
        public async Task UpdateAutomobileAsync(AutomobileTbl automobile)
        {
            await _uow.AutomobileRepository.UpdateAsync(automobile);
        }

    }

}