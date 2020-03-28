using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.IService
{
    public interface IAutomobileTypeService
    {
        void AddNewAutomobileType(AutomobileTypeTbl automobileType);
        IList<AutomobileTypeTbl> GetAllAutomobileTypes();
        AutomobileTypeTbl GetAutomobileType(int? id);
        AutomobileTypeTbl GetAutomobileTypeByCoolerBus(int cooler, int bus);
        bool Delete(int id);
        void UpdateAutomobileType(AutomobileTypeTbl automobileType);

        Task AddNewAutomobileTypeAsync(AutomobileTypeTbl AutomobileType, CancellationToken ct = new CancellationToken());
        Task<IList<AutomobileTypeTbl>> GetAllAutomobileTypesAsync(CancellationToken ct = new CancellationToken());
        Task<AutomobileTypeTbl> GetAutomobileTypeAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<AutomobileTypeTbl> GetAutomobileTypeByCoolerBus(int cooler, int bus, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
        Task UpdateAutomobileTypeAsync(AutomobileTypeTbl automobileType);
    }
}
