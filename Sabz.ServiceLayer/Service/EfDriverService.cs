using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sabz.DataLayer.Context;
using Sabz.DataLayer.IRepository;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.IService;
using Sabz.ServiceLayer.Mapper; 

namespace Sabz.ServiceLayer.Service
{
    public class EfDriverService : IDriverService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public EfDriverService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }

        public IList<DriverTbl> GetAllDrivers()
        {
            return _uow.DriverRepository.GetAll(x => x.IsActive).ToList();
        }
        public IList<DriverTbl> GetAllDriversByIds(List<int> ids)
        {
            return _uow.DriverRepository.GetAll(x =>ids.Contains(x.Id) && x.IsActive).ToList();
        }
        public IList<DriverTbl> GetOtherDriversByIds(List<int> ids)
        {
            return _uow.DriverRepository.GetAll(x => !ids.Contains(x.Id) && x.IsActive).ToList();
        }
        public DriverTbl GetDriver(int? id)
        {
            return _uow.DriverRepository.GetAll(x => x.IsActive && x.Id==id).FirstOrDefault();
        }
        public DriverTbl GetDriverByName(string fullName)
        {
            return _uow.DriverRepository.GetAll(x => x.IsActive && x.FullName.Contains(fullName)).FirstOrDefault();
        }
        public void AddNewDriver(DriverTbl driver)
        {
            _uow.DriverRepository.Add(driver);
            _uow.SaveAllChanges();
        }
        public bool Delete(int id)
        {
            DriverTbl driver = _uow.DriverRepository.Get(id);
            var t = _uow.DriverRepository.SoftDelete(driver);
            _uow.SaveAllChanges();
            return t;
        }
        public DriverTbl UpdateDriver(DriverTbl entity)
        {
            entity = _uow.DriverRepository.Update(entity);
            _uow.SaveAllChanges();
            return entity;
        }
        ////Async 
        public async Task<IList<DriverTbl>> GetAllDriversAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.DriverRepository.GetAllAsync(ct);
            return obj.ToList();
        }
        public async Task<IList<DriverTbl>> GetAllDriversByIdsAsync(List<int> ids)
        {
            var obj = await _uow.DriverRepository.GetAllAsync(x => ids.Contains(x.Id) && x.IsActive);
            return obj.ToList();
        }
        public async Task<IList<DriverTbl>> GetOtherDriversByIdsAsync(List<int> ids)
        {
            var obj = await _uow.DriverRepository.GetAllAsync(x => !ids.Contains(x.Id) && x.IsActive);
            return obj.ToList();
        }
        public async Task<DriverTbl> GetDriverAsync(int? id)
        {
            return await _uow.DriverRepository.GetAsync(x => x.IsActive && x.Id == id);
        }
        public async Task<DriverTbl> GetDriverByNameAsync(string fullName)
        {
            var obj = await _uow.DriverRepository.GetAllAsync(x => x.IsActive && x.FullName.Contains(fullName));
            return obj.FirstOrDefault();
        }
        public async Task AddNewDriverAsync(DriverTbl Driver, CancellationToken ct = new CancellationToken())
        {
            await _uow.DriverRepository.AddAsync(Driver, ct);
            _uow.SaveAllChanges();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Driver = await _uow.DriverRepository.GetAsync(id, ct);
            var obj = await _uow.DriverRepository.SoftDeleteAsync(Driver);
            _uow.SaveAllChanges();
            return obj;
        }
        public async Task<DriverTbl> UpdateDriverAsync(DriverTbl entity)
        {
            entity =await _uow.DriverRepository.UpdateAsync(entity);
            _uow.SaveAllChanges();
            return entity;
        }
    }
}
