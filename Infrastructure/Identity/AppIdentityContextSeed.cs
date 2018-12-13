using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public static class AppIdentityContextSeed
    {
        public static async Task<string> SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string userId = (Guid.NewGuid()).ToString();
            ApplicationUser appUser = await userManager.FindByNameAsync("admin");
            if (appUser == null)
            {
                var defaultUser = new ApplicationUser { UserName = "admin", Email = "admin@admin.example", Id = userId };
                await userManager.CreateAsync(defaultUser, "admin");

                var adminRole = await roleManager.FindByNameAsync("admin");
                if (adminRole == null)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = "admin" });
                }

                appUser = await userManager.FindByNameAsync(defaultUser.UserName);
                await userManager.AddToRoleAsync(appUser, "admin");

            } else
            {
                userId = appUser.Id;
            }

            return userId;
        }
    }
}
