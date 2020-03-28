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
    public class EfRegionService : IRegionService
    {
        IUnitOfWork _uow;
        public EfRegionService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void AddNewRegion(RegionTbl region)
        {
            _uow.RegionRepository.Add(region);
            _uow.SaveAllChanges();
        }
        public IList<RegionTbl> GetAllRegions()
        {
            return  _uow.RegionRepository.GetAll().ToList();
        }
        public RegionTbl GetRegion(int? id)
        {
            return _uow.RegionRepository.Get((int)id);
        }
        public bool Delete(int id)
        {
            RegionTbl region= _uow.RegionRepository.Get(id);
            var t = _uow.RegionRepository.SoftDelete(region);
            _uow.SaveAllChanges();
            return t;
        }
        public void UpdateRegion(RegionTbl region)
        {
            _uow.RegionRepository.Update(region);
        }
        //Async
        public async Task AddNewRegionAsync(RegionTbl region)
        {
            await _uow.RegionRepository.AddAsync(region);
            _uow.SaveAllChanges();
        }
        public async Task<IList<RegionTbl>> GetAllRegionsAsync()
        {
            var obj= await _uow.RegionRepository.GetAllAsync();
            return obj.ToList();
        }
        public async Task<RegionTbl> GetRegionAsync(int? id)
        {
            var obj = await _uow.RegionRepository.GetAsync((int)id);
            return obj;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            RegionTbl region = await _uow.RegionRepository.GetAsync(id);
            var t = await _uow.RegionRepository.SoftDeleteAsync(region);
            _uow.SaveAllChanges();
            return t;
        }
        public async Task UpdateRegionAsync(RegionTbl region)
        {
            await _uow.RegionRepository.UpdateAsync(region);
        }
    }
}
