using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace TennisBookings.Web.Data
{
    public static class SeedData
    {
        private const string AdminRole = "Admin";

        public static async Task SeedUsersAndRoles(UserManager<TennisBookingsUser> userManager, RoleManager<TennisBookingsRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(AdminRole))
            {
                var adminRole = new TennisBookingsRole { Name = AdminRole };
                await roleManager.CreateAsync(adminRole);
            }

            if (await userManager.FindByEmailAsync("admin@example.com") == null)
            {
                var user = new TennisBookingsUser
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com"
                };

                var result = await userManager.CreateAsync(user, "Password1!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, AdminRole);
                }
            }

            if (await userManager.FindByEmailAsync("member@example.com") == null)
            {
                var user = new TennisBookingsUser
                {
                    UserName = "member@example.com",
                    Email = "member@example.com",
                    Member = new Member
                    {
                        Forename = "Steve",
                        Surname = "Gordon",
                        JoinDate = DateTime.UtcNow.Date
                    }
                };

                await userManager.CreateAsync(user, "Password1!");
            }
        }
    }
}
