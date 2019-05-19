using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Blognet.Cms.Infrastructure.Identity
{
    public static class AppIdentityContextSeed
    {
        //@refactor
        public static async Task<string> SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string userId = "b1b614ce-9aed-4f5d-ac0a-d65bc956ee64"; //(Guid.NewGuid()).ToString();
            ApplicationUser appUser = await userManager.FindByNameAsync("admin");
            if (appUser != null) {
                await userManager.DeleteAsync(appUser);
                appUser = null;
            }

            var defaultUser = new ApplicationUser { UserName = "administrator", Email = "administrator@admin.example", Id = userId };
            await userManager.CreateAsync(defaultUser, "secret-password");

            var adminRole = await roleManager.FindByNameAsync("admin");
            if (adminRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "admin" });
            }

            appUser = await userManager.FindByNameAsync(defaultUser.UserName);
            await userManager.AddToRoleAsync(appUser, "admin");

            return appUser.Id;
        }
    }
}
