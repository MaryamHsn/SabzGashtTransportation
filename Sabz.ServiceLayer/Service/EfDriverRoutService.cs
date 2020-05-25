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
    public class EfDriverRoutService : IDriverRoutService
    {
        IUnitOfWork _uow;
        public EfDriverRoutService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void AddNewDriverRout(DriverRoutTbl driverRout)
        {
            _uow.DriverRoutRepository.Add(driverRout);
            _uow.SaveAllChanges();
        }
        public IList<DriverRoutTbl> GetAllDriverRouts()
        {
            return _uow.DriverRoutRepository.GetAll().ToList();
        }
        public IList<DriverRoutTbl> GetAllDriverRoutsByRegionId(int regionId)
        {
            return _uow.DriverRoutRepository.GetAll().Where(x => x.RoutTbl.RegionId == regionId).ToList();
        }
        public IList<DriverRoutTbl> GetAllDriverRoutsByIds(List<int> ids)
        {
            return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && ids.Contains(x.Id)).ToList();
        }
        public IList<DriverRoutTbl> GetAllDriverRoutsByRegionIdByIds(int regionId, List<int> ids)
        {
            return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && x.RoutTbl.RegionId == regionId && ids.Contains(x.Id)).ToList();
        }
        public DriverRoutTbl GetDriverRout(int? id)
        {
            return _uow.DriverRoutRepository.Get(x => x.Id == id);
        }
        public DriverRoutTbl GetDriverRoutByDriverIdRoutId(int driverId, int routId)
        {
            return _uow.DriverRoutRepository.Get(x => x.IsActive && x.DriverId == driverId && x.RoutId == routId);
        }

        public bool Delete(int id)
        {
            DriverRoutTbl driverRout = _uow.DriverRoutRepository.Get(id);
            var t = _uow.DriverRoutRepository.SoftDelete(driverRout);
            _uow.SaveAllChanges();
            return t;
        }
        public void UpdateDriverRout(DriverRoutTbl driverRout)
        {
            _uow.DriverRoutRepository.Update(driverRout);
        }
        public List<DriverRoutTbl> GetDriverRoutByRoutId(int routId)
        {
            return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && x.RoutId == routId).ToList();
        }
        public List<DriverRoutTbl> GetDriverRoutByRoutIds(List<int> routIds)
        {
            return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && routIds.Contains(x.RoutId)).ToList();
        }
        public List<DriverRoutTbl> GetDriverRoutByDriverName(string driverName)
        {
            return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && x.DriverTbl.FullName.Contains(driverName)).ToList();
        }
        public List<DriverRoutTbl> GetDriverRoutByDriverNameByRegionId(string driverName, int regionId)
        {
            return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && x.RoutTbl.RegionId == regionId && x.DriverTbl.FullName.Contains(driverName)).ToList();
        }
        //public List<DriverRoutTbl> GetDriverRoutByDateByDriverNameByRoutName(DateTime datFrom,DateTime dateTo,string driverName, string routName)
        //{
        //    return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && x.DriverTbl.FullName.Contains(driverName) && x.RoutTbl.Name.Contains(routName) && x.RoutTbl.StartDate >= datFrom && x.RoutTbl.StartDate <= dateTo).ToList();
        //}
        public List<DriverRoutTbl> GetDriverRoutByDateByDriverName(DateTime datFrom, DateTime dateTo, string driverName)
        {
            return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && x.DriverTbl.FullName.Contains(driverName) && x.RoutTbl.StartDate >= datFrom && x.RoutTbl.StartDate <= dateTo).ToList();
        }
        public List<DriverRoutTbl> GetDriverRoutByDateByDriverNameByRegionId(DateTime datFrom, DateTime dateTo, string driverName, int regionId)
        {
            return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && x.RoutTbl.RegionId == regionId && x.DriverTbl.FullName.Contains(driverName) && x.RoutTbl.StartDate >= datFrom && x.RoutTbl.StartDate <= dateTo).ToList();
        }
        public List<DriverRoutTbl> GetDriverRoutByDateByDriverIdByRegionId(DateTime datFrom, DateTime dateTo, int driverId, int regionId)
        {
            return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && x.RoutTbl.RegionId == regionId && x.DriverTbl.Id == driverId && x.RoutTbl.StartDate >= datFrom && x.RoutTbl.StartDate <= dateTo).ToList();
        }
        public List<DriverRoutTbl> GetDriverRoutByDateByDriverNameByRegionIdByIds(DateTime datFrom, DateTime dateTo, string driverName, int regionId, List<int> ids)
        {
            return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && x.RoutTbl.RegionId == regionId && x.DriverTbl.FullName.Contains(driverName) && x.RoutTbl.StartDate >= datFrom && x.RoutTbl.StartDate <= dateTo && ids.Contains(x.Id)).ToList();
        }
        public List<DriverRoutTbl> GetDriverRoutByDateByDriverIdByRegionIdByIds(DateTime datFrom, DateTime dateTo, int driverId, int regionId, List<int> ids)
        {
            return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && x.RoutTbl.RegionId == regionId && x.DriverTbl.Id == driverId && x.RoutTbl.StartDate >= datFrom && x.RoutTbl.StartDate <= dateTo && ids.Contains(x.Id)).ToList();
        }
        public List<DriverRoutTbl> GetDriverRoutByDateByRegionId(DateTime datFrom, DateTime dateTo, int regionId)
        {
            return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && x.RoutTbl.RegionId == regionId && x.RoutTbl.StartDate >= datFrom && x.RoutTbl.StartDate <= dateTo).ToList();
        }
        public List<DriverRoutTbl> GetDriverRoutByDateByRegionIdByIds(DateTime datFrom, DateTime dateTo, int regionId, List<int> ids)
        {
            return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && x.RoutTbl.RegionId == regionId && x.RoutTbl.StartDate >= datFrom && x.RoutTbl.StartDate <= dateTo && ids.Contains(x.Id)).ToList();
        }
        //public List<DriverRoutTbl> GetDriverRoutByDateByRoutName(DateTime datFrom, DateTime dateTo, string routName)
        //{
        //    return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && x.RoutTbl.Name.Contains(routName) && x.RoutTbl.StartDate >= datFrom && x.RoutTbl.StartDate <= dateTo).ToList();
        //}
        //public List<DriverRoutTbl> GetDriverRoutByDate(DateTime datFrom, DateTime dateTo)
        //{
        //    var t =_uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && x.RoutTbl.StartDate >= datFrom && x.RoutTbl.StartDate <= dateTo).ToList();
        //    return t;
        //}
        //public List<DriverRoutTbl> GetDriverRoutByRoutName(string routName)
        //{
        //    return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && x.RoutTbl.Name.Contains(routName)).ToList();
        //}
        //public List<DriverRoutTbl> GetDriverRoutByDriverNameRoutName(string routName,string DriverName)
        //{
        //    return _uow.DriverRoutRepository.GetAll().Where(x => x.IsActive && x.DriverTbl.FullName.Contains(DriverName) && x.RoutTbl.Name.Contains(routName)).ToList();
        //}
        ////Async 
        public async Task AddNewDriverRoutAsync(DriverRoutTbl DriverRout, CancellationToken ct = new CancellationToken())
        {
            await _uow.DriverRoutRepository.AddAsync(DriverRout, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<DriverRoutTbl>> GetAllDriverRoutsAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.DriverRoutRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<IList<DriverRoutTbl>> GetAllDriverRoutsByRegionIdAsync(int regionId)
        {
            var list = await _uow.DriverRoutRepository.GetAllAsync();
            return list.Where(x => x.RoutTbl.RegionId == regionId).ToList();
        }
        public async Task<IList<DriverRoutTbl>> GetAllDriverRoutsByIdsAsync(List<int> ids)
        {
            var obj = await _uow.DriverRoutRepository.GetAllAsync();
            return obj.Where(x => x.IsActive && ids.Contains(x.Id)).ToList();
        }
        public async Task<DriverRoutTbl> GetDriverRoutAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.DriverRoutRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<DriverRoutTbl> GetDriverRoutByDriverIdRoutIdAsync(int driverId, int routId)
        {
            return await _uow.DriverRoutRepository.GetAsync(x => x.IsActive && x.DriverId == driverId && x.RoutId == routId);
        }
        public async Task<List<DriverRoutTbl>> GetDriverRoutByRoutIdAsync(int routId)
        {
            var obj = await _uow.DriverRoutRepository.GetAllAsync(x => x.IsActive && x.RoutId == routId);
            return obj.ToList();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var DriverRout = await _uow.DriverRoutRepository.GetAsync(id, ct);
            var obj = await _uow.DriverRoutRepository.SoftDeleteAsync(DriverRout);
            _uow.SaveAllChanges();
            return obj;
        }
        public async Task UpdateDriverRoutAsync(DriverRoutTbl driverRout)
        {
            await _uow.DriverRoutRepository.UpdateAsync(driverRout);
        }
        public async Task<List<DriverRoutTbl>> GetDriverRoutByRoutIdsAsync(List<int> routIds)
        {
            var list = await _uow.DriverRoutRepository.GetAllAsync(x => x.IsActive && routIds.Contains(x.RoutId));
            return list.ToList();
        }
        public async Task<List<DriverRoutTbl>> GetDriverRoutByDriverNameAsync(string driverName)
        {
            var list = await _uow.DriverRoutRepository.GetAllAsync(x => x.IsActive && x.DriverTbl.FullName.Contains(driverName));
            return list.ToList();
        }
        public async Task<List<DriverRoutTbl>> GetDriverRoutByDriverNameByRegionIdAsync(string driverName, int regionId)
        {
            var list = await _uow.DriverRoutRepository.GetAllAsync();
            return list.Where(x => x.IsActive && x.RoutTbl.RegionId == regionId && x.DriverTbl.FullName.Contains(driverName)).ToList();

        }
        //public async Task<List<DriverRoutTbl>> GetDriverRoutByDateByDriverNameByRoutNameAsync(DateTime datFrom, DateTime dateTo, string driverName, string routName)
        //{
        //    var list = await _uow.DriverRoutRepository.GetAllAsync(x => x.IsActive && x.DriverTbl.FullName.Contains(driverName) && x.RoutTbl.Name.Contains(routName) && x.RoutTbl.StartDate >= datFrom && x.RoutTbl.StartDate <= dateTo);
        //    return list.ToList();
        //}
        public async Task<List<DriverRoutTbl>> GetDriverRoutByDateByDriverNameAsync(DateTime datFrom, DateTime dateTo, string driverName)
        {
            var list = await _uow.DriverRoutRepository.GetAllAsync();
            return list.Where(x => x.IsActive && x.DriverTbl.FullName.Contains(driverName) && x.RoutTbl.StartDate >= datFrom && x.RoutTbl.StartDate <= dateTo).ToList();
        }
        public async Task<List<DriverRoutTbl>> GetDriverRoutByDateByDriverNameByRegionIdAsync(DateTime datFrom, DateTime dateTo, string driverName, int regionId)
        {
            var list = await _uow.DriverRoutRepository.GetAllAsync();
            return list.Where(x => x.IsActive && x.RoutTbl.RegionId == regionId && x.DriverTbl.FullName.Contains(driverName) && x.RoutTbl.StartDate >= datFrom && x.RoutTbl.StartDate <= dateTo).ToList();
        }
        public async Task<List<DriverRoutTbl>> GetDriverRoutByDateByRegionIdAsync(DateTime datFrom, DateTime dateTo, int regionId)
        {
            var list = await _uow.DriverRoutRepository.GetAllAsync();
            return list.Where(x => x.IsActive && x.RoutTbl.RegionId == regionId && x.RoutTbl.StartDate >= datFrom && x.RoutTbl.StartDate <= dateTo).ToList();
        }
        //public async Task<List<DriverRoutTbl>> GetDriverRoutByDateByRoutNameAsync(DateTime datFrom, DateTime dateTo, string routName)
        //{
        //    var list = await _uow.DriverRoutRepository.GetAllAsync(x => x.IsActive && x.RoutTbl.Name.Contains(routName) && x.RoutTbl.StartDate >= datFrom && x.RoutTbl.StartDate <= dateTo);
        //    return list.ToList();

        //}
        public async Task<List<DriverRoutTbl>> GetDriverRoutByDateAsync(DateTime datFrom, DateTime dateTo)
        {
            var list = await _uow.DriverRoutRepository.GetAllAsync(x => x.IsActive && x.RoutTbl.StartDate >= datFrom && x.RoutTbl.StartDate <= dateTo);
            return list.ToList();

        }
        // public async Task<List<DriverRoutTbl>> GetDriverRoutByRoutNameAsync(string routName)
        //{
        //    var list = await _uow.DriverRoutRepository.GetAllAsync(x => x.IsActive && x.RoutTbl.Name.Contains(routName));
        //    return list.ToList();

        //}
        //public async Task<List<DriverRoutTbl>> GetDriverRoutByDriverNameRoutNameAsync(string routName, string DriverName)
        //{
        //    var list = await _uow.DriverRoutRepository.GetAllAsync(x => x.IsActive && x.DriverTbl.FullName.Contains(DriverName) && x.RoutTbl.Name.Contains(routName));
        //    return list.ToList();

        //}
    }
}
