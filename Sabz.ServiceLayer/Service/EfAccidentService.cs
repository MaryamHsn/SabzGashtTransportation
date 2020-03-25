using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sabz.DataLayer.Context;
using Sabz.DataLayer.IRepository;
using Sabz.DataLayer.Repository;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.IService;

namespace Sabz.ServiceLayer.Service
{ 
    public class EfAccidentService :  IAccidentService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public EfAccidentService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewAccident(AccidentTbl accident)
        {
            _uow.AccidentRepository.Add(accident);
            _uow.SaveAllChanges();
        }
        public IList<AccidentTbl> GetAllAccidents()
        {
            return _uow.AccidentRepository.GetAll().ToList();
        }
        public AccidentTbl GetAccident(int? id)
        {
            return _uow.AccidentRepository.GetAll(x=>x.Id==id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            AccidentTbl accident = _uow.AccidentRepository.Get(id);
            var t =_uow.AccidentRepository.SoftDelete(accident);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewAccidentAsync(AccidentTbl accident, CancellationToken ct = new CancellationToken())
        {
            await   _uow.AccidentRepository.AddAsync(accident,ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<AccidentTbl>> GetAllAccidentsAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.AccidentRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<AccidentTbl>  GetAccidentAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj= await _uow.AccidentRepository.GetAllAsync(x=>x.Id==id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var accident = await _uow.AccidentRepository.GetAsync(id, ct);
            var obj =await _uow.AccidentRepository.SoftDeleteAsync(accident);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
