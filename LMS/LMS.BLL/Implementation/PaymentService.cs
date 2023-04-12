using LMS.BLL.Interfaces;
using LMS.DAL.Entities;
using LMS.Repository;
using Microsoft.Extensions.Configuration;
using PayStack.Net;

namespace LMS.BLL.Implementation;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Student> _studRepo;
    private readonly IConfiguration _configuration;
    private readonly string token;

    private PayStackApi PayStack { get; set; }

    public PaymentService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _studRepo = unitOfWork.GetRepository<Student>();
        _configuration = configuration;
    }

    public Task<TransactionVerifyResponse> VerifyPayment(string reference)
    {
        throw new Exceptions.NotImplementedException("here");
    }


    private static int Generate()
    {
        Random rand = new Random((int)DateTime.Now.Ticks);
        return rand.Next(100000000, 999999999);
    }
}
