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
    public class RepairmentRepository : BaseRepository<RepairmentTbl, int>, IRepairmentRepository
    {
        public RepairmentRepository(ApplicationDbContext contextFactory) : base(contextFactory)
        {
        }
    }
}
