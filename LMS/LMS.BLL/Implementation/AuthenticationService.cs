using AutoMapper;
using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;
using LMS.BLL.Interfaces;
using LMS.DAL.Entities.identityEntities;
using LMS.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IServiceFactory _serviceFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        // private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
     //   private IRepository<ApplicationRoleClaim> _roleClaimsRepo;
        private readonly IRepository<AppUser> _userRepo;
        private readonly RoleManager<AppRole> _roleManager;

        public AuthenticationService(IServiceFactory serviceFactory, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _serviceFactory = serviceFactory;
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
          //  _mapper = _serviceFactory.GetService<IMapper>();
            //  _emailService = _serviceFactory.GetService<IEmailService>();
            _userManager = userManager;
            _roleManager = roleManager;
         //   _roleClaimsRepo = _unitOfWork.GetRepository<ApplicationRoleClaim>();
            _userRepo = _unitOfWork.GetRepository<AppUser>();
        }
        public async Task<string> CreateUser(UserRegistrationRequest request)
        {
            AppUser existingUser = await _userManager.FindByEmailAsync(request.Email);
            
            if (existingUser != null)
                throw new InvalidOperationException($"User already exists with Email {request.Email}");

            existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
                throw new InvalidOperationException($"User already exists with username {request.UserName}");

            AppUser user = new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = request.Email.ToLower(),
                UserName = request.UserName.Trim().ToLower(),
                
                PhoneNumber = request.MobileNumber,
               
            };

            //string password = AuthenticationExtension.GenerateRandomPassword();

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to create user: {(result.Errors.FirstOrDefault())?.Description}");
            }

          //  await _userManager.SetTwoFactorEnabledAsync(user, true);


            AddUserToRoleRequest userRole = new() { UserName = user.UserName, Role = request.Role };

            await _serviceFactory.GetService<IRoleService>().AddUserToRole(userRole);

            //UserMailRequest userMailDto = new()
            //{
            //    User = user,
            //    FirstName = request.Firstname
            //};

            //await _emailService.SendCreateUserEmail(userMailDto);
            return user.Id;
        }

        public Task<AuthenticationResponse> UserLogin(LoginRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
