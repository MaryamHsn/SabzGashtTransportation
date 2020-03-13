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
    public class DriverRepository : BaseRepository<DriverTbl, int>, IDriverRepository
    {
        public DriverRepository(ApplicationDbContext contextFactory) : base(contextFactory)
        {
        }
    }
}
