using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;
using TPSS.Data.Repository;

namespace TPSS.Business.Service.Impl
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository) 
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<int> CreatePaymentAsync(string contractId)
        {
            try
            {
                Payment payment = new Payment();
                payment.ContractId = contractId;
                payment.PaymentId = await AutoGeneratePaymentId();
                payment.CommissionCalculation = 0;//2% Price
                payment.Status = "Processing";
                payment.Amount = 0;//Price
                var result = await _paymentRepository.CreatePaymentAsync(payment);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<dynamic> GetPaymentDetailAsync(string paymentId)
        {
            try
            {
                var result = await _paymentRepository.GetPaymentDetailAsync(paymentId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }

        }

        public async Task<IEnumerable<dynamic>> GetPaymentForBuyerAsync(string contractId, string userId)
        {
            try
            {
                var result = await _paymentRepository.GetPaymentForBuyerAsync(contractId,userId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<dynamic>> GetPaymentForSellerAsync(string contractId, string userId)
        {
            try
            {
                var result = await _paymentRepository.GetPaymentForBuyerAsync(contractId, userId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        private async Task<string> AutoGeneratePaymentId()
        {
            string newPaymenttId = "";
            string latestPaymentId = await _paymentRepository.GetLatestPaymentIdAsync();
            if (latestPaymentId.IsNullOrEmpty())
            {
                newPaymenttId = "CT00000000";
            }
            else
            {
                int numericpart = int.Parse(latestPaymentId.Substring(2));
                int newnumericpart = numericpart + 1;
                newPaymenttId = $"CT{newnumericpart:d8}";
            }
            return newPaymenttId;
        }
    }
}
