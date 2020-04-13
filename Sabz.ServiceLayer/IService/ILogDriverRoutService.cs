using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.IService
{
    public interface ILogDriverRoutService
    {
        void AddNewLogDriverRout(LogDriverRoutTbl LogDriverRout);
        IList<LogDriverRoutTbl> GetAllLogDriverRouts();
        IList<LogDriverRoutTbl> GetAllLogDriverRoutsByRegionId(int regionId);
        IList<LogDriverRoutTbl> GetAllLogDriverRoutsByDriverName(string driverName);
        IList<LogDriverRoutTbl> GetAllLogDriverRoutsByDriverNameByDate(string driverName, DateTime fromDate, DateTime toDate);
        IList<LogDriverRoutTbl> GetAllLogDriverRoutsByDriverNameByRegionId(string driverName, int regionId);
        IList<LogDriverRoutTbl> GetAllLogDriverRoutsByDriverNameByDateByRegionId(string driverName, int regionId, DateTime fromDate, DateTime toDate);
        IList<LogDriverRoutTbl> GetAllLogDriverRoutsByDateByRegionId(int regionId, DateTime fromDate, DateTime toDate);
        LogDriverRoutTbl GetLogDriverRout(int? id);
        bool Delete(int id);
        void UpdateLogDriverRout(LogDriverRoutTbl logDriverRout);
        Task AddNewLogDriverRoutAsync(LogDriverRoutTbl driverRout);
        Task<IList<LogDriverRoutTbl>> GetAllLogDriverRoutsAsync();
        Task<IList<LogDriverRoutTbl>> GetAllLogDriverRoutsByRegionIdAsync(int regionId);
        Task<IList<LogDriverRoutTbl>> GetAllLogDriverRoutsByDriverNameAsync(string driverName);
        Task<IList<LogDriverRoutTbl>> GetAllLogDriverRoutsByDriverNameByDateAsync(string driverName, DateTime fromDate, DateTime toDate);
        Task<IList<LogDriverRoutTbl>> GetAllLogDriverRoutsByDriverNameByRegionIdAsync(string driverName, int regionId);
        Task<IList<LogDriverRoutTbl>> GetAllLogDriverRoutsByDriverNameByDateByRegionIdAsync(string driverName, int regionId, DateTime fromDate, DateTime toDate);
        Task<IList<LogDriverRoutTbl>> GetAllLogDriverRoutsByDateByRegionIdAsync(int regionId, DateTime fromDate, DateTime toDate);
        Task<LogDriverRoutTbl> GetLogDriverRoutAsync(int? id);
        Task<bool> DeleteAsync(int id);
        Task UpdateLogDriverRoutAsync(LogDriverRoutTbl logDriverRout);
    }
}
