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
        DriverTbl GetDriver(int? id);
        DriverTbl GetDriverByName(string fullName);
        void AddNewDriver(DriverTbl driverRout);
        int Delete(int id);
    }
}
