using LMS.DAL.Entities;
using LMS.DAL.Entities.identityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.DAL
{
    public class LMSAppDbContext : IdentityDbContext<AppUser, AppRole, string, AppUserClaim, AppUserRole,
        IdentityUserLogin<string>, AppRoleClaim, IdentityUserToken<string>>
    {
        public LMSAppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserLogin<string>>().HasNoKey();
            builder.Entity<IdentityUserRole<string>>().HasNoKey();
            builder.Entity<IdentityUserToken<string>>().HasNoKey();

            builder.Entity<Assessment>().HasOne(x => x.Student)
                .WithMany(c => c.Assessments)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Assessment>().HasOne(c => c.CourseFor)
                .WithMany(c => c.Assessments)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CompletedStudentsCourses>()
                .HasOne(s => s.Student)
                .WithMany(a => a.CompletedCourses)
                .HasForeignKey(f => f.StudentId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CompletedStudentsCourses>()
                .HasOne(c => c.Course)
                .WithMany(c => c.StudentsCompleted)
                .HasForeignKey(fk => fk.CourseId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<EnrolledStudentsCourses>()
                .HasOne(s => s.Student)
                .WithMany(a => a.EnrolledCourses)
                .HasForeignKey(f => f.StudentId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<EnrolledStudentsCourses>()
                .HasOne(c => c.Course)
                .WithMany(c => c.EnrolledStudents)
                .HasForeignKey(fk => fk.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Course>().Property(c => c.Price).HasConversion(typeof(double));


            builder.Entity<Assessment>().Property(c => c.Score).HasConversion(typeof(decimal));
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Assessment> Assessments { get; set; }
    }
}
