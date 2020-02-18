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
        readonly IDbSet<PaymentTbl> _payments;
        public EfPaymentService(IUnitOfWork uow)
        {
            _uow = uow;
            _payments = _uow.Set<PaymentTbl>();
        }

        public void AddNewPayment(PaymentTbl payment)
        {
            _payments.Add(payment);
        }

        public IList<PaymentTbl> GetAllPayments()
        {
            return _payments.ToList();
        }
        public PaymentTbl GetPayment(int? id)
        {
            return _payments.Find(id);
        }

        public int Delete(int id)
        {
            PaymentTbl payment = _payments.Find(id);
            payment.IsActive = false;
            return payment.PaymentId;
        }
    }
}
