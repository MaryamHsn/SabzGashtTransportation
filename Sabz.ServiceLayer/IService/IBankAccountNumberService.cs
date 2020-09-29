using Sabz.DomainClasses.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sabz.ServiceLayer.IService
{
    public interface IBankAccountNumberService
    {
        void AddNewBankAccountNumber(BankAccountNumberTbl automobileType);
        IList<BankAccountNumberTbl> GetAllBankAccountNumbers();
        BankAccountNumberTbl GetBankAccountNumber(int? id);
        List<BankAccountNumberTbl> GetBankAccountNumberByRegionId(int regionId);
        List<BankAccountNumberTbl> GetBankAccountNumberByDriverId(int driverId);
        BankAccountNumberTbl GetBankAccountNumberByDriverIdByRegionId(int driverId, int regionId);
        bool Delete(int id);
        void UpdateBankAccountNumber(BankAccountNumberTbl automobileType);
        List<DriverTbl> GetDriverByRegionId(int regionId);
        List<DriverTbl> GetDriverByExceptRegionId(int regionId);
        List<DriverTbl> GetDriverByRegionIds(List<int> regionIds); 
        List<BankAccountNumberTbl> GetBankAccountNumberByExceptRegionId(int regionId);
        Task AddNewBankAccountNumberAsync(BankAccountNumberTbl BankAccountNumber, CancellationToken ct = new CancellationToken());
        Task<IList<BankAccountNumberTbl>> GetAllBankAccountNumbersAsync(CancellationToken ct = new CancellationToken());
        Task<BankAccountNumberTbl> GetBankAccountNumberAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<List<BankAccountNumberTbl>> GetBankAccountNumberByRegionIdAsync(int regionId);
        Task<List<BankAccountNumberTbl>> GetBankAccountNumberByDriverIdAsync(int driverId);
        Task<BankAccountNumberTbl> GetBankAccountNumberByDriverIdByRegionIdAsync(int driverId, int regionId);
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
        Task UpdateBankAccountNumberAsync(BankAccountNumberTbl automobileType);
    }
}
