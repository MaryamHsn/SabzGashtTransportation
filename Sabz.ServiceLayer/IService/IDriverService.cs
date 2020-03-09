using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        int Delete(int id);
    }
}
