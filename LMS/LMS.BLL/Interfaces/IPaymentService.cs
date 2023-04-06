using LMS.BLL.DTOs.Request;
using PayStack.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Interfaces
{
    public interface IPaymentService
    {
        Task<TransactionInitializeResponse> InitalizePayment(PaymentRequest request);
        Task<TransactionVerifyResponse> VerifyPayment(string reference);

       /* Task<IEnumerable<Transaction>> GetPayments();
        Task<Transaction> GetPaymentByid(string id);*/
    }
}
