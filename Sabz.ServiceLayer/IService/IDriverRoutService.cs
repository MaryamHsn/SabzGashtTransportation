using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.IService
{
    public interface IDriverRoutService
    {
        void AddNewDriverRout(DriverRoutTbl DriverRout);
        IList<DriverRoutTbl> GetAllDriverRouts();
        DriverRoutTbl GetDriverRout(int? id);
        DriverRoutTbl GetDriverRoutByDriverIdRoutId(int driverId, int routId);
        List<DriverRoutTbl> GetDriverRoutByRoutId(int routId);
        bool Delete(int id);
        Task AddNewDriverRoutAsync(DriverRoutTbl DriverRout, CancellationToken ct = new CancellationToken());
        Task<IList<DriverRoutTbl>> GetAllDriverRoutsAsync(CancellationToken ct = new CancellationToken());
        Task<DriverRoutTbl> GetDriverRoutAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<DriverRoutTbl> GetDriverRoutByDriverIdRoutIdAsync(int driverId, int routId);
        Task<List<DriverRoutTbl>> GetDriverRoutByRoutIdAsync(int routId);
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}
