using LMS.BLL.Extensions;
using LMS.BLL.Infrastructure;
using LMS.BLL.Infrastructures.jwt;
using LMS.DAL;
using LMS.DAL.Entities.identityEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace LMS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LEARNING MANAGEMENT SYSTEM", Version = "v1" });
                


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            },
    });
            });



            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(jwt =>
                    {

                        JwtConfig jwtConfig = builder.Configuration.GetSection(nameof(JwtConfig)).Get<JwtConfig>();
                        var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

                        jwt.SaveToken = true;
                        jwt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            RequireExpirationTime = true,
                            ValidIssuer = jwtConfig.Issuer,
                            ValidAudience = jwtConfig.Audience,
                            ClockSkew = TimeSpan.Zero
                        };
                    });

            builder.Services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("Authorization", policy => policy.Requirements.Add(new AuthorizationRequirment()));
            });

            builder.Services.AddDbContext<LMSAppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn"), m => m.MigrationsAssembly("LMS.DAL"));

            });




            // builder.Services.AddScoped<AutoMapper(Assembly.Load("LMS.DAL.Entities"))>
            //  builder.Services.AddAutoMapper()
            builder.Services.AddAutoMapper(Assembly.Load("LMS.DAL"));
            builder.Services.RegisterServices();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddIdentity<AppUser, AppRole>(options =>
            options.SignIn.RequireConfirmedAccount = false).AddDefaultTokenProviders()
            .AddEntityFrameworkStores<LMSAppDbContext>();

            builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.AddGlobalErrorHandler();

            app.Run();
        }
    }
}