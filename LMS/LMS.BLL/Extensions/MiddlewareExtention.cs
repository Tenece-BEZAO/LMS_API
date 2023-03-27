using LMS.BLL.Implementation;
using LMS.BLL.Interfaces;
using LMS.DAL;
using LMS.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.BLL.Extensions
{
    public static class MiddlewareExtention
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork<LMSAppDbContext>>();
            services.AddScoped<ICourseService, CourseService>();


        }
    }
}
