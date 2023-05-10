using PayStack.Net;

namespace LMS.BLL.Interfaces;

public interface IPaymentService
{
    Task<TransactionVerifyResponse> VerifyPayment(string reference);

    /* Task<IEnumerable<Transaction>> GetPayments();
     Task<Transaction> GetPaymentByid(string id);*/
}
