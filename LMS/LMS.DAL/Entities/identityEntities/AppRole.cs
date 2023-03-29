using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities.identityEntities
{
    public class AppRole  : IdentityRole
    {

        public bool Active { get; set; } = true;
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
