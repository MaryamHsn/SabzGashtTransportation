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
        public DriverRoutTbl GetDriverRout(int? id)
        {
            return _uow.DriverRoutRepository.Get(x =>x.Id==id);
        }
        public DriverRoutTbl GetDriverRoutByDriverIdRoutId(int driverId,int routId)
        {
            return _uow.DriverRoutRepository.Get(x => x.IsActive && x.DriverId == driverId && x.RoutId==routId);
        }
        public List<DriverRoutTbl> GetDriverRoutByRoutId(int routId)
        {
            return _uow.DriverRoutRepository.GetAll(x => x.IsActive && x.RoutId == routId).ToList();
        }
        public bool Delete(int id)
        {
            DriverRoutTbl driverRout = _uow.DriverRoutRepository.Get(id);
            var t = _uow.DriverRoutRepository.SoftDelete(driverRout);
            _uow.SaveAllChanges();
            return t;
        }

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
            var obj= await _uow.DriverRoutRepository.GetAllAsync(x => x.IsActive && x.RoutId == routId);
            return obj.ToList();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var DriverRout = await _uow.DriverRoutRepository.GetAsync(id, ct);
            var obj = await _uow.DriverRoutRepository.SoftDeleteAsync(DriverRout);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
