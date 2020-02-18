using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.IService
{
    public interface ILogRoutDriverService
    {
        void AddNewLogRoutDriver(LogRoutDriverTbl LogRoutDriver);
        IList<LogRoutDriverTbl> GetAllLogRoutDrivers();
        LogRoutDriverTbl GetLogRoutDriver(int? id);
        int Delete(int id);
    }
}
