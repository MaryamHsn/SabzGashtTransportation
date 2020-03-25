using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.IService
{
    public interface IDriverService
    {
        IList<DriverTbl> GetAllDrivers();
        IList<DriverTbl> GetAllDriversByIds(List<int> ids);
        IList<DriverTbl> GetOtherDriversByIds(List<int> ids);
        DriverTbl GetDriver(int? id);
        DriverTbl GetDriverByName(string fullName);
        void AddNewDriver(DriverTbl driverRout);
        bool Delete(int id);
        DriverTbl UpdateDriver(DriverTbl entity);
        Task<IList<DriverTbl>> GetAllDriversAsync(CancellationToken ct = new CancellationToken());
        Task<IList<DriverTbl>> GetAllDriversByIdsAsync(List<int> ids);
        Task<IList<DriverTbl>> GetOtherDriversByIdsAsync(List<int> ids);
        Task<DriverTbl> GetDriverAsync(int? id);
        Task<DriverTbl> GetDriverByNameAsync(string fullName);
        Task AddNewDriverAsync(DriverTbl Driver, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
        Task<DriverTbl> UpdateDriverAsync(DriverTbl entity);
    }
}
