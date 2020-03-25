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
        RoutTbl GetRout(int? id);
       // RoutTbl GetRoutByName(string name);
        bool Delete(int id);
        Task AddNsewRoutAsync(RoutTbl rout);
        Task<IList<RoutTbl>> GetAllRoutsAsync();
        Task<RoutTbl> GetRoutAsync(int? id);
        Task<bool> DeleteAsync(int id);
    }
}
