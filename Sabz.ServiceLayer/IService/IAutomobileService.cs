using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.IService
{
    public interface IAutomobileService
    {
        void AddNewAutomobile(AutomobileTbl automobile);
        IList<AutomobileTbl> GetAllAutomobiles();
        AutomobileTbl GetAutomobile(int? id);
        bool Delete(int id);
        void UpdateAutomobile(AutomobileTbl automobile);
        Task AddNewAutomobileAsync(AutomobileTbl Automobile, CancellationToken ct = new CancellationToken());
        Task<IList<AutomobileTbl>> GetAllAutomobilesAsync(CancellationToken ct = new CancellationToken());
        Task<AutomobileTbl> GetAutomobileAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
        Task UpdateAutomobileAsync(AutomobileTbl automobile);
    }
}
