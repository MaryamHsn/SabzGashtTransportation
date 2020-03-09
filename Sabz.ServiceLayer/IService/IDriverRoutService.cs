using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        int Delete(int id);
    }
}
