using Microsoft.AspNetCore.Identity;

namespace LMS.DAL.Entities.identityEntities
{
    public class AppUserLogin : IdentityUserLogin<string>
    {
        public int Id { get; set; }
    }
}