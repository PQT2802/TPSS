using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository
{
    public interface IPaymentRepository
    {
        public Task<int> CreatePaymentAsync(Payment newPayment);
        public Task<IEnumerable<dynamic>> GetPaymentForSellerAsync(string contractId, string userId);
        public Task<IEnumerable<dynamic>> GetPaymentForBuyerAsync(string contractId, string userId);
        public Task<dynamic> GetPaymentDetailAsync(string paymentId);
        public Task<string> GetLatestPaymentIdAsync();
    }
}
