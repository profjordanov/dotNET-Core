using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Autofac;
using TennisBookings.Web.Core.DependencyInjection;
using TennisBookings.Web.Core.Middleware;
using TennisBookings.Web.Data;
using TennisBookings.Web.Services;

namespace TennisBookings.Web
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
            services.AddAppConfiguration(Configuration)                
                .AddBookingServices()
                .AddBookingRules()
                .AddCourtUnavailability()
                .AddMembershipServices()
                .AddStaffServices()
                //.AddCourtServices() - replaced with Autofac registration in ConfigureContainer
                .AddWeatherForecasting()
                .AddNotifications()                
                .AddGreetings()
                .AddCaching()
                .AddTimeServices()
                .AddAuditing();
            
            services.Configure<CookiePolicyOptions>(options =>
            {                
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizePage("/FindAvailableCourts");
                    options.Conventions.AuthorizePage("/BookCourt");
                    options.Conventions.AuthorizePage("/Bookings");
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddIdentity<TennisBookingsUser, TennisBookingsRole>()
                .AddEntityFrameworkStores<TennisBookingDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddDbContext<TennisBookingDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseLastRequestTracking(); // only track requests which make it to MVC, i.e. not static files
            app.UseMvc();
        }

        // This method is called after ConfigureServices
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<CourtMaintenanceService>()
                .As<ICourtMaintenanceService>()
                .InstancePerLifetimeScope();
        }
    }
}
