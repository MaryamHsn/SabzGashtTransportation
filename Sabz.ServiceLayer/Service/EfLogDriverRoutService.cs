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


    }
}
