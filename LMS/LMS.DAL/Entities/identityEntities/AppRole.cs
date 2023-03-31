using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities.identityEntities
{
    public class AppRole  : IdentityRole 
    {

        public bool Active { get; set; }

        [NotMapped]
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }

    }
}
