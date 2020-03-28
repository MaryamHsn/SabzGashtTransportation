using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabz.DataLayer.Context;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.IService;

namespace Sabz.ServiceLayer.Service
{
    public class EfPaymentService : IPaymentService 
    {
        IUnitOfWork _uow; 
        public EfPaymentService(IUnitOfWork uow)
        {
            _uow = uow; 
        }

        public void AddNewPayment(PaymentTbl payment)
        {
            _uow.PaymentRepository.Add(payment);
            _uow.SaveAllChanges();
        }

        public IList<PaymentTbl> GetAllPayments()
        {
            return _uow.PaymentRepository.GetAll().ToList();
        }
        public PaymentTbl GetPayment(int? id)
        {
            return _uow.PaymentRepository.Get((int)id);
        }

        public bool Delete(int id)
        {
            PaymentTbl payment = _uow.PaymentRepository.Get(id); 
            var t = _uow.PaymentRepository.SoftDelete(payment);
            _uow.SaveAllChanges();
            return t;
        }
        //Async
        public async Task AddNewPaymentAsync(PaymentTbl payment)
        {
            await _uow.PaymentRepository.AddAsync(payment);
            _uow.SaveAllChanges();
        }

        public async Task<IList<PaymentTbl>> GetAllPaymentsAsync()
        {
            var obj = await _uow.PaymentRepository.GetAllAsync();
            return obj.ToList();
        }
        public async Task<PaymentTbl> GetPaymentAsync(int? id)
        {
            return await _uow.PaymentRepository.GetAsync((int)id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            PaymentTbl payment = await _uow.PaymentRepository.GetAsync(id);
            var t =await _uow.PaymentRepository.SoftDeleteAsync(payment);
            _uow.SaveAllChanges();
            return t;
        }
    }
}
