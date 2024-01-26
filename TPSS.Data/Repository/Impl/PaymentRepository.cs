using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Helper;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository.Impl
{
    public class PaymentRepository : BaseRepository, IPaymentRepository
    {
        public PaymentRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Task<int> CreatePaymentAsync(Payment newPayment)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletePaymentAsync(string PaymentId)
        {
            throw new NotImplementedException();
        }

        public Task<Payment> GetPaymentByIdAsync(string PaymentID)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePaymentAsync(Payment updatePayment)
        {
            throw new NotImplementedException();
        }
    }
}