using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;
using LMS.DAL.Entities.identityEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> CreateUser(UserRegistrationRequest request);
        Task<AuthenticationResponse> UserLogin(LoginRequest request);

        Task<string> ChangePassword(string userId, ChangePasswordRequest request);
        Task<string> ResetPassword(ResetPasswordRequest request);
        Task<IEnumerable<AppUser>> GetAllUsers ();

        //Task<string> CreateUser(UserRegistrationRequest request);
        //Task<AuthenticationResponse> UserLogin(LoginRequest request);
        //Task<AuthenticationResponse> ConfirmTwoFactorToken(TwoFactorLoginRequest request);
        //Task<string> VerifyUser(VerifyAccountRequest request);
        //Task<string> ChangeEmail(ChangeEmailRequest request);
        //Task ToggleUserActivation(string userId);
        //Task UpdateRecoveryEmail(string userId, string email);

        Task<UserResponse> GetUser(string userId);

        //Task<AuthenticationResponse> ImpersonateUser(ImpersonationLoginRequest request);
        //Task<ImpersonationLoginResponse> ImpersonationLogin(ImpersonationLoginRequest request);
    }
}
