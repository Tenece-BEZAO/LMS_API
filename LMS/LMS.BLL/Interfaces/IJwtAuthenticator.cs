
using System.Security.Claims;
using LMS.BLL.DTOs.Response;
using LMS.DAL.Entities.identityEntities;

namespace LMS.BLL.Interfaces
{

    public interface IJWTAuthenticator
    {
        Task<JwtToken> GenerateJwtToken(AppUser user, string expires = null, List<Claim> additionalClaims = null);
    }
}
