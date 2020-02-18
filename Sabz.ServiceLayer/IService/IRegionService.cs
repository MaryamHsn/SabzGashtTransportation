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
        int Delete(int id);
    }
}
