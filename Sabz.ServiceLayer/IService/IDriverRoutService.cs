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
        IList<DriverRoutTbl> GetAllDriverRoutsByRegionId(int regionId);
        IList<DriverRoutTbl> GetAllDriverRoutsByIds(List<int> ids);
        DriverRoutTbl GetDriverRout(int? id);
        DriverRoutTbl GetDriverRoutByDriverIdRoutId(int driverId, int routId);
        List<DriverRoutTbl> GetDriverRoutByRoutId(int routId);
        List<DriverRoutTbl> GetDriverRoutByRoutIds(List<int> routIds);
        bool Delete(int id);
        void UpdateDriverRout(DriverRoutTbl driverRout);
        List<DriverRoutTbl> GetDriverRoutByDriverName(string driverName);
        List<DriverRoutTbl> GetDriverRoutByDriverNameByRegionId(string driverName, int regionId);
       // List<DriverRoutTbl> GetDriverRoutByDateByDriverNameByRoutName(DateTime datFrom, DateTime dateTo, string driverName, string routName);
        List<DriverRoutTbl> GetDriverRoutByDateByDriverName(DateTime datFrom, DateTime dateTo, string driverName);
        List<DriverRoutTbl> GetDriverRoutByDateByDriverNameByRegionId(DateTime datFrom, DateTime dateTo, string driverName, int regionId);
        List<DriverRoutTbl> GetDriverRoutByDateByRegionId(DateTime datFrom, DateTime dateTo, int regionId);

        //List<DriverRoutTbl> GetDriverRoutByDateByRoutName(DateTime datFrom, DateTime dateTo, string routName);
        //List<DriverRoutTbl> GetDriverRoutByDate(DateTime datFrom, DateTime dateTo);
        //List<DriverRoutTbl> GetDriverRoutByRoutName(string routName);
        //List<DriverRoutTbl> GetDriverRoutByDriverNameRoutName(string routName, string DriverName);

        Task AddNewDriverRoutAsync(DriverRoutTbl DriverRout, CancellationToken ct = new CancellationToken());
        Task<IList<DriverRoutTbl>> GetAllDriverRoutsAsync(CancellationToken ct = new CancellationToken());
        Task<IList<DriverRoutTbl>> GetAllDriverRoutsByRegionIdAsync(int regionId);
        Task<IList<DriverRoutTbl>> GetAllDriverRoutsByIdsAsync(List<int> ids);
        Task<DriverRoutTbl> GetDriverRoutAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<DriverRoutTbl> GetDriverRoutByDriverIdRoutIdAsync(int driverId, int routId);
        Task<List<DriverRoutTbl>> GetDriverRoutByRoutIdAsync(int routId);
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
        Task UpdateDriverRoutAsync(DriverRoutTbl driverRout);
        Task<List<DriverRoutTbl>> GetDriverRoutByRoutIdsAsync(List<int> routIds);
        Task<List<DriverRoutTbl>> GetDriverRoutByDriverNameAsync(string driverName);
        Task<List<DriverRoutTbl>> GetDriverRoutByDriverNameByRegionIdAsync(string driverName, int regionId);
        //Task<List<DriverRoutTbl>> GetDriverRoutByDateByDriverNameByRoutNameAsync(DateTime datFrom, DateTime dateTo, string driverName, string routName);
        Task<List<DriverRoutTbl>> GetDriverRoutByDateByDriverNameAsync(DateTime datFrom, DateTime dateTo, string driverName);
        Task<List<DriverRoutTbl>> GetDriverRoutByDateByDriverNameByRegionIdAsync(DateTime datFrom, DateTime dateTo, string driverName, int regionId);
        Task<List<DriverRoutTbl>> GetDriverRoutByDateByRegionIdAsync(DateTime datFrom, DateTime dateTo, int regionId);
        // Task<List<DriverRoutTbl>> GetDriverRoutByDateByRoutNameAsync(DateTime datFrom, DateTime dateTo, string routName);
        Task<List<DriverRoutTbl>> GetDriverRoutByDateAsync(DateTime datFrom, DateTime dateTo);
        //Task<List<DriverRoutTbl>> GetDriverRoutByRoutNameAsync(string routName);
        //Task<List<DriverRoutTbl>> GetDriverRoutByDriverNameRoutNameAsync(string routName, string DriverName);
    }
}
