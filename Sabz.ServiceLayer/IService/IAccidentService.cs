using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.IService
{
    public interface IAccidentService
    {
        void AddNewAccident(AccidentTbl accident);
        IList<AccidentTbl> GetAllAccidents();
        AccidentTbl GetAccident(int? id);
      //  int Delete(int id);
        bool Delete(int  id);
        Task AddNewAccidentAsync(AccidentTbl accident, CancellationToken ct = new CancellationToken());
        Task<IList<AccidentTbl>> GetAllAccidentsAsync(CancellationToken ct = new CancellationToken());
        Task<AccidentTbl> GetAccidentAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}
