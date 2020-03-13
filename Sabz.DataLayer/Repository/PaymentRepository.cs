using Sabz.DataLayer.Context;
using Sabz.DataLayer.IRepository;
using Sabz.DataLayer.Repository;
using Sabz.DomainClasses.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabz.DataLayer.Repository
{
    public class PaymentRepository : BaseRepository<PaymentTbl, int>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext contextFactory) : base(contextFactory)
        {
        }
    }
}
