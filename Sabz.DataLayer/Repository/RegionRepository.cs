using Sabz.DataLayer.Context;
using Sabz.DataLayer.IRepository;
using Sabz.DomainClasses.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabz.DataLayer.Repository
{
    public class RegionRepository : BaseRepository<RegionTbl, int>, IRegionRepository
    {
        public RegionRepository(ApplicationDbContext contextFactory) : base(contextFactory)
        {
        }
    }
}
