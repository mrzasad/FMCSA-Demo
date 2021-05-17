using MediatR;
using Core.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Infrastructure.AutoMapper;
using AutoMapper;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Application.Services;
using Presentation.Models.Common.Security;
using Presentation.Security;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Microsoft.Extensions.Hosting;
using Application.CQRS.VehicleInfo;

namespace Presentation
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
            services.AddMvcCore()
           .AddAuthorization();
           
            services.AddControllers()
            .AddNewtonsoftJson();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //Auto Mapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            // Add DbContext using SQL Server Provider
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            services.AddMvc(option => option.EnableEndpointRouting = false)
               .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //Add MediatR
            services.AddMediatR(typeof(GetVehicleInfoListQuery).GetTypeInfo().Assembly);

            services.AddScoped(typeof(IUserManager), typeof(UserManager));
            // session service 
            services.AddHttpContextAccessor();
            services.AddTransient<SessionContext>();
            services.AddSession(so =>
            {
                so.IdleTimeout = TimeSpan.FromSeconds(60 * 30);
                so.Cookie.IsEssential = true; // make the session cookie Essential
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CustomAuthorization", policy =>
                    policy.AddRequirements(new CustomAuthorization()));
            });

            services.AddSingleton<IAuthorizationHandler,
                CustomHandler>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Account/Index";
                    });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Account/Index";
                options.LogoutPath = $"/Account/Logout";
                options.AccessDeniedPath = $"/Account/AccessDenied";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseSession();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseAuthentication();

            app.UseMvc();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=index}/{id?}");
            });
        }
    }
}
