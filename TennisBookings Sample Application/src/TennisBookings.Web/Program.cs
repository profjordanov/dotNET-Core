using System;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TennisBookings.Web.Data;

namespace TennisBookings.Web
{
    public class Program
    {
        public static async Task Main (string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();

            using (var scope = webHost.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var hostingEnvironment = serviceProvider.GetRequiredService<IHostingEnvironment>();
                var appLifetime = serviceProvider.GetRequiredService<IApplicationLifetime>();
                
                if (hostingEnvironment.IsDevelopment())
                {
                    var ctx = serviceProvider.GetRequiredService<TennisBookingDbContext>();
                    await ctx.Database.MigrateAsync(appLifetime.ApplicationStopping);

                    try
                    {
                        var userManager = serviceProvider.GetRequiredService<UserManager<TennisBookingsUser>>();
                        var roleManager = serviceProvider.GetRequiredService<RoleManager<TennisBookingsRole>>();

                        await SeedData.SeedUsersAndRoles(userManager, roleManager);
                    }
                    catch (Exception ex)
                    {
                        var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("UserInitialisation");
                        logger.LogError(ex, "Failed to seed user data");
                    }
                }
            }

            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>();
    }
}
