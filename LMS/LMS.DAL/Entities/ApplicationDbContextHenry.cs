using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities
{
    public class ApplicationDbContextHenry : DbContext
    {
        public ApplicationDbContextHenry(DbContextOptions<ApplicationDbContextHenry> options): base (options)
        {

        }
        DbSet<DummyCourseHenry> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DummyCourseHenry>()
                .Property(p => p.Description)
                .IsRequired(false)
                .HasMaxLength(1000);
            modelBuilder.Entity<DummyCourseHenry>()
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<DummyCourseHenry>()
                .Property(p => p.CoursePdfsUrl)
                .IsRequired();
            modelBuilder.Entity<DummyCourseHenry>()
               .Property(p => p.CourseVideoUrl)
               .IsRequired();
            modelBuilder.Entity<DummyCourseHenry>()
               .Property(p => p.CourseCode)
               .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
