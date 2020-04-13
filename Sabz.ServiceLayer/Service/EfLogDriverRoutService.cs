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
    public class EfLogDriverRoutService : ILogDriverRoutService
    {
        IUnitOfWork _uow;
        public EfLogDriverRoutService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void AddNewLogDriverRout(LogDriverRoutTbl driverRout)
        {
            _uow.LogDriverRoutRepository.Add(driverRout);
            _uow.SaveAllChanges();
        }

        public IList<LogDriverRoutTbl> GetAllLogDriverRouts()
        {
            return _uow.LogDriverRoutRepository.GetAll().ToList();
        }
        public IList<LogDriverRoutTbl> GetAllLogDriverRoutsByRegionId(int regionId)
        {
            return _uow.LogDriverRoutRepository.GetAll().Where(x=>x.DriverRoutTbl.RoutTbl.RegionId== regionId).ToList();
        }
        public IList<LogDriverRoutTbl> GetAllLogDriverRoutsByDriverName(string driverName)
        {
            return _uow.LogDriverRoutRepository.GetAll().Where(x => x.DriverRoutTbl.DriverTbl.FullName.Contains(driverName)).ToList();
        }
        public IList<LogDriverRoutTbl> GetAllLogDriverRoutsByDriverNameByDate(string driverName, DateTime fromDate, DateTime toDate)
        {
            return _uow.LogDriverRoutRepository.GetAll().Where(x => x.DriverRoutTbl.DriverTbl.FullName.Contains(driverName) && x.DriverRoutTbl.RoutTbl.StartDate >= fromDate && x.DriverRoutTbl.RoutTbl.StartDate <= toDate).ToList();
        }
        public IList<LogDriverRoutTbl> GetAllLogDriverRoutsByDriverNameByRegionId(string driverName, int regionId)
        {
            return _uow.LogDriverRoutRepository.GetAll().Where(x => x.DriverRoutTbl.DriverTbl.FullName.Contains(driverName) && x.DriverRoutTbl.RoutTbl.RegionId == regionId).ToList();
        }
        public IList<LogDriverRoutTbl> GetAllLogDriverRoutsByDriverNameByDateByRegionId(string driverName, int regionId, DateTime fromDate, DateTime toDate)
        {
            return _uow.LogDriverRoutRepository.GetAll().Where(x => x.DriverRoutTbl.DriverTbl.FullName.Contains(driverName) && x.DriverRoutTbl.RoutTbl.RegionId == regionId && x.DriverRoutTbl.RoutTbl.StartDate >= fromDate && x.DriverRoutTbl.RoutTbl.StartDate <= toDate).ToList();
        }
        public IList<LogDriverRoutTbl> GetAllLogDriverRoutsByDateByRegionId(int regionId, DateTime fromDate, DateTime toDate)
        {
            return _uow.LogDriverRoutRepository.GetAll().Where(x =>  x.DriverRoutTbl.RoutTbl.RegionId == regionId && x.DriverRoutTbl.RoutTbl.StartDate >= fromDate && x.DriverRoutTbl.RoutTbl.StartDate <= toDate).ToList();
        }
        public LogDriverRoutTbl GetLogDriverRout(int? id)
        {
            return _uow.LogDriverRoutRepository.Get((int)id);
        }

        public bool Delete(int id)
        {
            LogDriverRoutTbl logDriverRout = _uow.LogDriverRoutRepository.Get(id);
            var t = _uow.LogDriverRoutRepository.SoftDelete(logDriverRout);
            _uow.SaveAllChanges();
            return t;
        }
        public void UpdateLogDriverRout(LogDriverRoutTbl logDriverRout)
        {
            _uow.LogDriverRoutRepository.Update(logDriverRout);
        }
        //Async
        public async Task AddNewLogDriverRoutAsync(LogDriverRoutTbl driverRout)
        {
            await _uow.LogDriverRoutRepository.AddAsync(driverRout);
            _uow.SaveAllChanges();
        }

        public async Task<IList<LogDriverRoutTbl>> GetAllLogDriverRoutsAsync()
        {
            var obj = await _uow.LogDriverRoutRepository.GetAllAsync();
            return obj.ToList();
        }
        public async Task<IList<LogDriverRoutTbl>> GetAllLogDriverRoutsByRegionIdAsync(int regionId)
        {
            var obj = await _uow.LogDriverRoutRepository.GetAllAsync();
            return obj.Where(x => x.DriverRoutTbl.RoutTbl.RegionId == regionId).ToList();
        }
        public async Task<IList<LogDriverRoutTbl>> GetAllLogDriverRoutsByDriverNameAsync(string driverName)
        {
            var entities = await _uow.LogDriverRoutRepository.GetAllAsync();
            return entities.Where(x => x.DriverRoutTbl.DriverTbl.FullName.Contains(driverName)).ToList();
        }
        public async Task<IList<LogDriverRoutTbl>> GetAllLogDriverRoutsByDriverNameByDateAsync(string driverName, DateTime fromDate, DateTime toDate)
        {
            var entities = await _uow.LogDriverRoutRepository.GetAllAsync();
            return entities.Where(x => x.DriverRoutTbl.DriverTbl.FullName.Contains(driverName) && x.DriverRoutTbl.RoutTbl.StartDate >= fromDate && x.DriverRoutTbl.RoutTbl.StartDate <= toDate).ToList();
        }
        public async Task<IList<LogDriverRoutTbl>> GetAllLogDriverRoutsByDriverNameByRegionIdAsync(string driverName, int regionId)
        {
            var entities = await _uow.LogDriverRoutRepository.GetAllAsync();
            return entities.Where(x => x.DriverRoutTbl.DriverTbl.FullName.Contains(driverName) && x.DriverRoutTbl.RoutTbl.RegionId == regionId).ToList();
        }
        public async Task<IList<LogDriverRoutTbl>> GetAllLogDriverRoutsByDriverNameByDateByRegionIdAsync(string driverName, int regionId, DateTime fromDate, DateTime toDate)
        {
            var entities = await _uow.LogDriverRoutRepository.GetAllAsync();
            return entities.Where(x => x.DriverRoutTbl.DriverTbl.FullName.Contains(driverName) && x.DriverRoutTbl.RoutTbl.RegionId == regionId && x.DriverRoutTbl.RoutTbl.StartDate >= fromDate && x.DriverRoutTbl.RoutTbl.StartDate <= toDate).ToList();
        }
        public async Task<IList<LogDriverRoutTbl>> GetAllLogDriverRoutsByDateByRegionIdAsync(int regionId, DateTime fromDate, DateTime toDate)
        {
            var entities = await _uow.LogDriverRoutRepository.GetAllAsync();
            return entities.Where(x => x.DriverRoutTbl.RoutTbl.RegionId == regionId && x.DriverRoutTbl.RoutTbl.StartDate >= fromDate && x.DriverRoutTbl.RoutTbl.StartDate <= toDate).ToList();
        }
        public async Task<LogDriverRoutTbl> GetLogDriverRoutAsync(int? id)
        {
            return await _uow.LogDriverRoutRepository.GetAsync((int)id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            LogDriverRoutTbl logDriverRout = await _uow.LogDriverRoutRepository.GetAsync(id);
            var t = await _uow.LogDriverRoutRepository.SoftDeleteAsync(logDriverRout);
            _uow.SaveAllChanges();
            return t;
        }
        public async Task UpdateLogDriverRoutAsync(LogDriverRoutTbl logDriverRout)
        {
            await _uow.LogDriverRoutRepository.UpdateAsync(logDriverRout);
        }
    }
}
