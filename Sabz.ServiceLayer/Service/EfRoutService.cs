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

        //Async
        public async Task AddNsewRoutAsync(RoutTbl rout)
        {
            await _uow.RoutRepository.AddAsync(rout);
            _uow.SaveAllChanges();
        }
        public async Task<IList<RoutTbl>> GetAllRoutsAsync()
        {
            var obj= await _uow.RoutRepository.GetAllAsync();
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
    }
}
