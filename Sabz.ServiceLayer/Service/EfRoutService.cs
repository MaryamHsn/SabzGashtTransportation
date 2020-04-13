using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabz.DataLayer.Context;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.IService;

namespace Sabz.ServiceLayer.Service
{
    public class EfRoutService : IRoutService
    {
        IUnitOfWork _uow;
        public EfRoutService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void AddNewRout(RoutTbl rout)
        {
            _uow.RoutRepository.Add(rout);
            _uow.SaveAllChanges();
        }
        public IList<RoutTbl> GetAllRouts()
        {
            return _uow.RoutRepository.GetAll().ToList();
        }
        public IList<RoutTbl> GetAllRoutsByDate(DateTime? fromDate)
        {
            return _uow.RoutRepository.GetAll(x => DbFunctions.TruncateTime(x.StartDate)== DbFunctions.TruncateTime(fromDate)).ToList();
        }
        public IList<RoutTbl> GetAllRoutsByDateByRegionId(DateTime? fromDate,int regionId)
        {
            return _uow.RoutRepository.GetAll(x => DbFunctions.TruncateTime(x.StartDate)== DbFunctions.TruncateTime(fromDate) && x.RegionId==regionId).ToList();
        }
        public IList<RoutTbl> GetAllRoutsByDateFromByRegionId(DateTime? fromDate,int regionId)
        {
            return _uow.RoutRepository.GetAll(x => DbFunctions.TruncateTime(x.StartDate)>= DbFunctions.TruncateTime(fromDate) && x.RegionId==regionId).ToList();
        }
        public IList<RoutTbl> GetAllRoutsByDateFromByDateToByRegionId(DateTime? fromDate,DateTime? toDate,int regionId)
        {
            return _uow.RoutRepository.GetAll(x => DbFunctions.TruncateTime(x.StartDate)>= DbFunctions.TruncateTime(fromDate) &&DbFunctions.TruncateTime(x.StartDate)<= DbFunctions.TruncateTime(toDate) && x.RegionId==regionId).ToList();
        }
        public RoutTbl GetRout(int? id)
        {
            return _uow.RoutRepository.Get((int)id);
        }
        //public RoutTbl GetRoutByName(string name)
        //{
        //    return _uow.RoutRepository.Get(x => x.IsActive && x.Name == name).FirstOrDefault();
        //}
        public bool Delete(int id)
        {
            RoutTbl rout = _uow.RoutRepository.Get(id);
            var t = _uow.RoutRepository.SoftDelete(rout);            
            _uow.SaveAllChanges();
            return t;
        }
        public void UpdateRout(RoutTbl rout)
        {
            _uow.RoutRepository.Update(rout);
        }
        //public IList<RoutTbl> GetAllRoutsByDateByRoutName(DateTime date, string routName)
        //{
        //    // return _uow.RoutRepository.GetAll().Where(x => x.Name.Contains(routName)&& DbFunctions.TruncateTime(x.StartDate)== DbFunctions.TruncateTime(date)).ToList();
        //    var entities = _uow.RoutRepository.GetAll(x => DbFunctions.TruncateTime(x.StartDate) == DbFunctions.TruncateTime(date)).ToList();
        //    return entities.Where(x => x.Name.Contains(routName)).ToList();
        //    //var entities = _uow.RoutRepository.GetAll().Where(x => x.Name.Contains(routName)).ToList();
        //    //return entities.Where(x => DbFunctions.TruncateTime(x.StartDate) == DbFunctions.TruncateTime(date)).ToList();
        //}
        //public IList<RoutTbl> GetAllRoutsByRoutName(string routName)
        //{
        //    return _uow.RoutRepository.GetAll().Where(x => x.Name.Contains(routName)).ToList();
        //}
        //Async
        public async Task AddNsewRoutAsync(RoutTbl rout)
        {
            await _uow.RoutRepository.AddAsync(rout);
            _uow.SaveAllChanges();
        }
        public async Task<IList<RoutTbl>> GetAllRoutsAsync()
        {
            var obj = await _uow.RoutRepository.GetAllAsync();
            return obj.ToList();
        }
        public async Task<RoutTbl> GetRoutAsync(int? id)
        {
            return await _uow.RoutRepository.GetAsync((int)id);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            RoutTbl rout = await _uow.RoutRepository.GetAsync(id);
            var t = await _uow.RoutRepository.SoftDeleteAsync(rout);
            _uow.SaveAllChanges();
            return t;
        }
        public async Task UpdateRoutAsync(RoutTbl rout)
        {
            await _uow.RoutRepository.UpdateAsync(rout);
        }
        public async Task<IList<RoutTbl>> GetAllRoutsByDateAsync(DateTime? fromDate)
        {
            var list = await _uow.RoutRepository.GetAllAsync(x => DbFunctions.TruncateTime(x.StartDate) == DbFunctions.TruncateTime(fromDate));
            return list.ToList();
        }
        public async Task<IList<RoutTbl>> GetAllRoutsByDateByRegionIdAsync(DateTime? fromDate, int regionId)
        {
            var entity= await _uow.RoutRepository.GetAllAsync(x => DbFunctions.TruncateTime(x.StartDate) == DbFunctions.TruncateTime(fromDate) && x.RegionId == regionId);
            return entity.ToList();
        }
        public async Task<IList<RoutTbl>> GetAllRoutsByDateFromByRegionIdAsync(DateTime? fromDate, int regionId)
        {
            var entity = await _uow.RoutRepository.GetAllAsync(x => DbFunctions.TruncateTime(x.StartDate) >= DbFunctions.TruncateTime(fromDate) && x.RegionId == regionId);
            return entity.ToList();
        }
        public async Task<IList<RoutTbl>> GetAllRoutsByDateFromByDateToByRegionIdAsync(DateTime? fromDate, DateTime? toDate, int regionId)
        {
            var entity = await _uow.RoutRepository.GetAllAsync(x => DbFunctions.TruncateTime(x.StartDate) >= DbFunctions.TruncateTime(fromDate) && DbFunctions.TruncateTime(x.StartDate) <= DbFunctions.TruncateTime(toDate) && x.RegionId == regionId);
            return entity.ToList();
        }
        //public async Task<IList<RoutTbl>> GetAllRoutsByDateByRoutNameAsync(DateTime date, string routName)
        //{
        //    var entity = await _uow.RoutRepository.GetAllAsync();
        //    return entity.Where(x => x.StartDate.Date == date.Date && x.Name.Contains(routName)).ToList();
        //}
        //public async Task<IList<RoutTbl>> GetAllRoutsByRoutNameAsync(string routName)
        //{
        //    var entity = await _uow.RoutRepository.GetAllAsync();
        //    return entity.Where(x => x.Name.Contains(routName)).ToList();
        //}
    }
}
