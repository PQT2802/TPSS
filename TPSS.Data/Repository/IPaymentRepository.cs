using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository
{
    internal interface IPaymentRepository
    {
        public Task<Payment> GetPaymentByIdAsync(string PaymentID);

        public Task<int> CreatePaymentAsync(Payment newPayment);

        public Task<int> UpdatePaymentAsync(Payment updatePayment);

        public Task<int> DeletePaymentAsync(string PaymentId);
    }
}
