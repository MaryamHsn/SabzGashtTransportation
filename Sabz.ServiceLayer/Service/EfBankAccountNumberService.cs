using Sabz.DataLayer.Context;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sabz.ServiceLayer.Service
{
    public class EfBankAccountNumberService : IBankAccountNumberService
    {
        IUnitOfWork _uow;
        public EfBankAccountNumberService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void AddNewBankAccountNumber(BankAccountNumberTbl bankAccountNumber)
        {
            _uow.BankAccountNumberRepository.Add(bankAccountNumber);
        }
        public IList<BankAccountNumberTbl> GetAllBankAccountNumbers()
        {
            return _uow.BankAccountNumberRepository.GetAll().ToList();
        }
        public BankAccountNumberTbl GetBankAccountNumber(int? id)
        {
            return _uow.BankAccountNumberRepository.Get((int)id);
        }
        public List<BankAccountNumberTbl> GetBankAccountNumberByRegionId(int regionId)
        {
            return _uow.BankAccountNumberRepository.GetAll(x => x.IsActive && x.RegionId== regionId).ToList();
        } 
        public List<BankAccountNumberTbl> GetBankAccountNumberByDriverId(int driverId)
        {
            return _uow.BankAccountNumberRepository.GetAll(x => x.IsActive && x.DriverId == driverId).ToList();
        } 
        public BankAccountNumberTbl GetBankAccountNumberByDriverIdByRegionId(int driverId,int regionId)
        {
            return _uow.BankAccountNumberRepository.GetAll(x => x.IsActive && x.DriverId == driverId && x.RegionId==regionId).FirstOrDefault();
        } 
        public bool Delete(int id)
        {
            BankAccountNumberTbl bankAccountNumber = _uow.BankAccountNumberRepository.Get(id);
            var t = _uow.BankAccountNumberRepository.SoftDelete(bankAccountNumber);
            _uow.SaveAllChanges();
            return t;
        }
        public void UpdateBankAccountNumber(BankAccountNumberTbl bankAccountNumber)
        {
            _uow.BankAccountNumberRepository.Update(bankAccountNumber);
        }

        ////Async 
        public async Task AddNewBankAccountNumberAsync(BankAccountNumberTbl BankAccountNumber, CancellationToken ct = new CancellationToken())
        {
            await _uow.BankAccountNumberRepository.AddAsync(BankAccountNumber, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<BankAccountNumberTbl>> GetAllBankAccountNumbersAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.BankAccountNumberRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<BankAccountNumberTbl> GetBankAccountNumberAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.BankAccountNumberRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<List<BankAccountNumberTbl>> GetBankAccountNumberByRegionIdAsync(int regionId)
        {
            var entity =await _uow.BankAccountNumberRepository.GetAllAsync(x => x.IsActive && x.RegionId == regionId);
            return entity.ToList();
        }
        public async Task<List<BankAccountNumberTbl>> GetBankAccountNumberByDriverIdAsync(int driverId)
        {
            var entity =await _uow.BankAccountNumberRepository.GetAllAsync(x => x.IsActive && x.DriverId == driverId);
            return entity.ToList();
        }
        public async Task<BankAccountNumberTbl> GetBankAccountNumberByDriverIdByRegionIdAsync(int driverId, int regionId)
        {
            var entity= await  _uow.BankAccountNumberRepository.GetAsync(x => x.IsActive && x.DriverId == driverId && x.RegionId == regionId);
            return entity;
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var BankAccountNumber = await _uow.BankAccountNumberRepository.GetAsync(id, ct);
            var obj = await _uow.BankAccountNumberRepository.SoftDeleteAsync(BankAccountNumber);
            _uow.SaveAllChanges();
            return obj;
        }
        public async Task UpdateBankAccountNumberAsync(BankAccountNumberTbl bankAccountNumber)
        {
            await _uow.BankAccountNumberRepository.UpdateAsync(bankAccountNumber);
        }
    }
}
