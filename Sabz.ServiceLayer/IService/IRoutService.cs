using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.IService
{
    public interface IRoutService
    {
        void AddNewRout(RoutTbl rout);
        IList<RoutTbl> GetAllRouts();
        IList<RoutTbl> GetAllRoutsByDate(DateTime? fromDate);
        IList<RoutTbl> GetAllRoutsByDateByRegionId(DateTime? fromDate, int regionId);
        IList<RoutTbl> GetAllRoutsByDateFromByRegionId(DateTime? fromDate, int regionId);
        IList<RoutTbl> GetAllRoutsByDateFromByDateToByRegionId(DateTime? fromDate, DateTime? toDate, int regionId);
        RoutTbl GetRout(int? id);
        // RoutTbl GetRoutByName(string name);
        bool Delete(int id);
        void UpdateRout(RoutTbl rout);
        // IList<RoutTbl> GetAllRoutsByDateByRoutName(DateTime date, string routName);
        // IList<RoutTbl> GetAllRoutsByRoutName(string routName);
        Task AddNsewRoutAsync(RoutTbl rout);
        Task<IList<RoutTbl>> GetAllRoutsAsync();
        Task<RoutTbl> GetRoutAsync(int? id);
        Task<bool> DeleteAsync(int id);
        Task UpdateRoutAsync(RoutTbl rout);
        Task<IList<RoutTbl>> GetAllRoutsByDateAsync(DateTime? fromDate);
        Task<IList<RoutTbl>> GetAllRoutsByDateByRegionIdAsync(DateTime? fromDate, int regionId);
        Task<IList<RoutTbl>> GetAllRoutsByDateFromByRegionIdAsync(DateTime? fromDate, int regionId);
        Task<IList<RoutTbl>> GetAllRoutsByDateFromByDateToByRegionIdAsync(DateTime? fromDate, DateTime? toDate, int regionId);

     //   Task<IList<RoutTbl>> GetAllRoutsByDateByRoutNameAsync(DateTime date, string routName);
     // Task<IList<RoutTbl>> GetAllRoutsByRoutNameAsync(string routName);
    }
}
