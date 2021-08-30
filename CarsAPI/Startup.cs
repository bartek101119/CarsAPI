using CarsAPI.Authorization;
using CarsAPI.Entities;
using CarsAPI.Middleware;
using CarsAPI.Models;
using CarsAPI.Models.Validators;
using CarsAPI.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var authenticationSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication")
                .Bind(authenticationSettings);

            services.AddSingleton(authenticationSettings);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false; // nie wymagamy tylko przez https
                cfg.SaveToken = true; // zapisany po stronie serwera
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    // parametry walidacji
                    ValidIssuer = authenticationSettings.JwtIssuer, // wydawca tokenu
                    ValidAudience = authenticationSettings.JwtIssuer, // jakie podmioty moga uzywac tego tokenu
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });


            services.AddControllers().AddFluentValidation();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddDbContext<CarDbContext>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ICarCompanyService, CarCompanyService>();
            services.AddCors(options => 
            {
                options.AddPolicy("FrontEndClient", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            } );
            services.AddScoped<IValidator<CreateCarDto>, CreateCarDtoValidator>();
            services.AddScoped<CarCompanySeeder>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IAuthorizationHandler, MinimumTwoCarsRequirementHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("MinimumTwoCars", builder => builder.AddRequirements(new MinimumTwoCarsRequirement(2)));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CarCompanySeeder seeder)
        {
            seeder.Seed();
            app.UseCors("FrontEndClient");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
