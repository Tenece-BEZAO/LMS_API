using AutoMapper;
using InventoryMg.BLL.DTOs;
using InventoryMg.BLL.DTOs.Request;
using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;
using LMS.BLL.Exceptions;
using LMS.BLL.Infrastructures.jwt;
using LMS.BLL.Interfaces;
using LMS.DAL.Entities.identityEntities;
using LMS.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IServiceFactory _serviceFactory;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        // private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
     //   private IRepository<ApplicationRoleClaim> _roleClaimsRepo;
        private readonly IRepository<AppUser> _userRepo;
        private readonly RoleManager<AppRole> _roleManager;

        public AuthenticationService(IServiceFactory serviceFactory, IConfiguration configuration, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _serviceFactory = serviceFactory;
            _configuration = configuration;
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


       //AddUserToRoleRequest userRole = new() { UserName = user.UserName, Role = request.Role };

       //await _serviceFactory.GetService<IRoleService>().AddUserToRole(userRole);


            //UserMailRequest userMailDto = new()
            //{
            //    User = user,
            //    FirstName = request.Firstname
            //};

            //await _emailService.SendCreateUserEmail(userMailDto);
            return user.Id;
        }


        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {

            var AllUsers = _userManager.Users.ToList();


            return AllUsers;
        }

        public async Task<AuthenticationResponse> UserLogin(LoginRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser == null)
                throw new NotFoundException($"Invalid email/password");

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, request.Password);
            if (!isCorrect)
            {
                throw new NotFoundException($"Invalid email/password");
            }

            var jwtToken = await GenerateJwtToken(existingUser);
            return new AuthenticationResponse()
            {
                JwtToken = jwtToken.Token,
                Result = jwtToken.Result,
                FullName = existingUser.UserName
            };

        }



        //public async Task<AuthResult> GetNewJwtRefreshToken(TokenRequest tokenRequest)
        //{
        //    var result = await VerifyAndGenerateToken(tokenRequest);
        //    if (result.Result == false)
        //        throw new Exceptions.NotImplementedException("Invalid Tokens");

        //    return result;
        //}
        private async Task<AuthResult> GenerateJwtToken(AppUser user)
        {
            var JwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            var claims = await GetAllValidClaims(user);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor();

            tokenDescriptor.Subject = new ClaimsIdentity(claims); 
            tokenDescriptor.Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JwtConfig:Expires").Value));
            tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            
            //Token descriptor
            //var tokenDescriptor = new SecurityTokenDescriptor()
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JwtConfig:ExpiryTimeFrame").Value)),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            //};

            var token = JwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = JwtTokenHandler.WriteToken(token);


            //refresh token

            //var refreshToken = new RefreshToken()
            //{
            //    JwtId = token.Id,
            //    Token = RandomStringGenrator(23),//generate a refresh token
            //    CreatedAt = DateTime.UtcNow,
            //    ExpiryDate = DateTime.UtcNow.AddMonths(6),
            //    IsRevoked = false,
            //    IsUsed = false,
            //    UserId = user.Id,

            //};
            //await _dbContext.RefreshTokens.AddAsync(refreshToken);
            //await _dbContext.SaveChangesAsync();


            return new AuthResult()
            {
                Result = true,
                Token = jwtToken,
               // RefreshToken = refreshToken.Token,
                Errors = null
            };
        }

        private async Task<IList<Claim>> GetAllValidClaims(AppUser user)
        {
            var _options = new IdentityOptions();
            var claims = new List<Claim>()
            {
                 new Claim("Id", user.Id),
                  new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
            };
            //getting claims that we have assigned to the user
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            //get the user role and add to the claims
            var userRoles = await _userManager.GetRolesAsync(user);

            //convert roles to claims
            foreach (var userRole in userRoles)
            {

                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));

                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }


            }
            return claims;

        }


        private string RandomStringGenrator(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz_";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

        }

        //private async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
        //{
        //    var jwtTokenHandler = new JwtSecurityTokenHandler();
        //    try
        //    {
        //        _tokenValidationParameters.ValidateLifetime = false;// for dev
        //        var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken);
        //        if (validatedToken is JwtSecurityToken jwtSecurityToken)
        //        {
        //            var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        //            if (result == false)
        //            {
        //                return null;
        //            }
        //            var utcExpiryDate = long.Parse(tokenInVerification.Claims
        //                .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

        //            var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);
        //            if (expiryDate > DateTime.Now)
        //            {
        //                return new AuthResult()
        //                {
        //                    Result = false,
        //                    Errors = new List<string>()
        //                    {
        //                        "Expired Token"
        //                    }
        //                };
        //            }

        //            var storedToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);
        //            if (storedToken == null)
        //            {
        //                return new AuthResult()
        //                {
        //                    Result = false,
        //                    Errors = new List<string>()
        //                    {
        //                        "Invalid Token"
        //                    }
        //                };
        //            }

        //            if (storedToken.IsUsed)
        //            {
        //                return new AuthResult()
        //                {
        //                    Result = false,
        //                    Errors = new List<string>()
        //                    {
        //                        "Invalid Token"
        //                    }
        //                };
        //            }

        //            if (storedToken.IsRevoked)
        //            {
        //                return new AuthResult()
        //                {
        //                    Result = false,
        //                    Errors = new List<string>()
        //                    {
        //                        "Invalid Token"
        //                    }
        //                };
        //            }

        //            var jti = tokenInVerification.Claims
        //                .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
        //            if (storedToken.JwtId != jti)
        //            {
        //                return new AuthResult()
        //                {
        //                    Result = false,
        //                    Errors = new List<string>()
        //                    {
        //                        "Invalid Token"
        //                    }
        //                };
        //            }

        //            if (storedToken.ExpiryDate < DateTime.UtcNow)
        //            {
        //                return new AuthResult()
        //                {
        //                    Result = false,
        //                    Errors = new List<string>()
        //                    {
        //                        "Expired Token"
        //                    }
        //                };
        //            }

        //            storedToken.IsUsed = true;
        //            _dbContext.RefreshTokens.Update(storedToken);
        //            await _dbContext.SaveChangesAsync();

        //            var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);
        //            return await GenerateJwtToken(dbUser);


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new AuthResult()
        //        {
        //            Result = false,
        //            Errors = new List<string>()
        //                    {
        //                        $"{ex.Message}",
        //                        $"{ex.StackTrace}"
        //                    }
        //        };
        //    }
        //    return new AuthResult()
        //    {
        //        Result = false,
        //        Errors = new List<string>()
        //                    {
        //                        $"server error",

        //                    }
        //    };
        //}

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp);
            return dateTimeVal;
        }

      public  async Task<string> ChangePassword(string userId, ChangePasswordRequest request)
        {
            return "";
           // throw new NotImplementedException();
        }

      public   Task<UserResponse> GetUser(string userId)
        {
            return null;
           // throw new NotImplementedException();
        }

        public Task<string> ResetPassword(ResetPasswordRequest request)
        {
            return null;
          //  throw new NotImplementedException();
        }
    }
}
