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
        LogDriverRoutTbl GetLogDriverRout(int? id);
        bool Delete(int id);
        Task AddNewLogDriverRoutAsync(LogDriverRoutTbl driverRout);
        Task<IList<LogDriverRoutTbl>> GetAllLogDriverRoutsAsync();
        Task<LogDriverRoutTbl> GetLogDriverRoutAsync(int? id);
        Task<bool> DeleteAsync(int id);
    }
}
