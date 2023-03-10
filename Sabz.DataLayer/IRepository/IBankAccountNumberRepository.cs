using Sabz.DataLayer.Repository;
using Sabz.DomainClasses.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabz.DataLayer.IRepository
{
    public interface IBankAccountNumberRepository : IRepository<BankAccountNumberTbl, int>
    {
    }
}
