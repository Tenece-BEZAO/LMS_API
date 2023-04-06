using LMS.BLL.DTOs.Request;
using LMS.BLL.Exceptions;
using LMS.BLL.Interfaces;
using LMS.DAL.Entities;
using LMS.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PayStack.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Student> _studRepo;
        private readonly IRepository<CoursePayment> _paymentRepo;
        private readonly IConfiguration _configuration;
        private readonly string token;

        private PayStackApi PayStack { get; set; }
        public PaymentService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _studRepo = unitOfWork.GetRepository<Student>();
            _paymentRepo = unitOfWork.GetRepository<CoursePayment>();
            _configuration = configuration;
        }
        public async Task<TransactionInitializeResponse> InitalizePayment(PaymentRequest request)
        {
            var user = _studRepo.GetById(request.StudentId);
            if (user == null)
                throw new NotFoundException($"Invalid Student Id");

            TransactionInitializeRequest createRequest = new()
            {
                AmountInKobo = request.Amount * 100,
                Email = request.Email,
                Currency = "NGN",
                Reference = Generate().ToString(),
                CallbackUrl = "https://localhost:7242/api/Payment/verify-payment"

            };
            TransactionInitializeResponse response = PayStack.Transactions.Initialize(createRequest);
            if (response.Status)
            {
                var transaction = new CoursePayment()
                {
                    Name = request.Name,
                    Amount = request.Amount,
                    TransactionRef = createRequest.Reference,
                    Email = request.Email,
                    Status = false,
                    StudentId = request.StudentId,
                    CourseId =  request.CourseId

                };
                await _paymentRepo.AddAsync(transaction);
                return response;

            }
            throw new Exceptions.NotImplementedException("the payment was unable to go through");
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
}
