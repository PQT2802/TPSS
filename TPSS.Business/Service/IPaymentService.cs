using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;

namespace TPSS.Business.Service
{
    public interface IPaymentService
    {
        public Task<int> CreatePaymentAsync(string contractId);
        public Task<IEnumerable<dynamic>> GetPaymentForSellerAsync(string contractId, string userId);
        public Task<IEnumerable<dynamic>> GetPaymentForBuyerAsync(string contractId, string userId);
        public Task<dynamic> GetPaymentDetailAsync(string paymentId);
    }
}
