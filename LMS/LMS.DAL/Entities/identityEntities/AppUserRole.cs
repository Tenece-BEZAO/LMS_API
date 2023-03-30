using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities.identityEntities
{
    public class AppUserRole : IdentityUserRole<string>
    {
       //public string UserId { get; set; }
       //public string RoleId { get; set; }

       //// [ForeignKey("UserId")]
       // public virtual AppUser User { get; set; }

       //// [ForeignKey("RoleId")]
       // public virtual AppRole Role { get; set; }
    }
}
