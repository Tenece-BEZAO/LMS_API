using LMS.BLL.Implementation;
using LMS.BLL.Infrastructure.jwt;
using LMS.BLL.Infrastructures.jwt;
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
            services.AddTransient<IJWTAuthenticator, JwtAuthenticator>();
            services.AddTransient<JwtConfig, JwtConfig>();
            // services.AddTransient<IAuthorizationHandler, CustomAuthorizationHandler>();
            services.AddTransient<IUnitOfWork, UnitOfWork<LMSAppDbContext>>();
            services.AddTransient<IServiceFactory, ServiceFactory>();
            services.AddTransient<IInstructorService, InstructorService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IPaymentService, PaymentService>();
            //services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<Interfaces.IAuthenticationService, Implementation.AuthenticationService>();
            services.AddTransient<IRoleService, RoleService>();


        }
    }
}
