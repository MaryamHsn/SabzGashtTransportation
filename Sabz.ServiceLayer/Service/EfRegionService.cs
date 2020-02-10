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
        readonly IDbSet<RegionTbl> _regions;
        public EfRegionService(IUnitOfWork uow)
        {
            _uow = uow;
            _regions = _uow.Set<RegionTbl>();
        }

        public void AddNewRegion(RegionTbl region)
        {
            _regions.Add(region);
        }
        public IList<RegionTbl> GetAllRegions()
        {
            return _regions.ToList();
        }
    }
}
