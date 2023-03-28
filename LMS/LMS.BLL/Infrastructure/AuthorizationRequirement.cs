using Microsoft.AspNetCore.Authorization;

namespace LMS.BLL.Infrastructure
{
    public class AuthorizationRequirment : IAuthorizationRequirement
    {
        public int Success { get; set; }
    }
}
