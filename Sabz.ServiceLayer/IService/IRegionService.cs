using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.IService
{
    public interface IRegionService
    {
        void AddNewRegion(RegionTbl Region);
        IList<RegionTbl> GetAllRegions();
        RegionTbl GetRegion(int? id);
        bool Delete(int id);
        void UpdateRegion(RegionTbl region);
        Task AddNewRegionAsync(RegionTbl region);
        Task<IList<RegionTbl>> GetAllRegionsAsync();
        Task<RegionTbl> GetRegionAsync(int? id);
        Task<bool> DeleteAsync(int id);
        Task UpdateRegionAsync(RegionTbl region);
    }
}
