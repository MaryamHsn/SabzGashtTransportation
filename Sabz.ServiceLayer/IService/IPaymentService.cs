using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.IService
{
    public interface IPaymentService
    {
        void AddNewPayment(PaymentTbl payment);
        IList<PaymentTbl> GetAllPayments();
        PaymentTbl GetPayment(int? id);
        bool Delete(int id);
        Task AddNewPaymentAsync(PaymentTbl payment);
        Task<IList<PaymentTbl>> GetAllPaymentsAsync();
        Task<PaymentTbl> GetPaymentAsync(int? id);
        Task<bool> DeleteAsync(int id);
    }
}
