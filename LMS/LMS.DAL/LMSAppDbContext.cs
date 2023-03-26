using LMS.DAL.Entities;
using LMS.DAL.Entities.identityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL
{
    public class LMSAppDbContext   : IdentityDbContext<AppUser, AppRole, string>
    {

        public LMSAppDbContext(DbContextOptions options): base(options)
        {

        }

        

        public DbSet<Course>   Courses { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Assessment> Assessments { get; set; }

    }
}
