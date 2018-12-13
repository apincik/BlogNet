using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Web.Extensions;

namespace Core.Extensions
{
    public static class ApplicationBuilderSeedExtension
    {
        /// <summary>
        /// Extension method for ApplicationBuilder to seed user data.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static async Task SeedData(this IApplicationBuilder builder)
        {
            IServiceProvider provider = builder.ApplicationServices;

            using (var scope = provider.GetService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string userId = await AppIdentityContextSeed.SeedAsync(userManager, roleManager);

                var webContext = scope.ServiceProvider.GetRequiredService<WebContext>();
                Project project = await WebContextSeed.SeedAsync(webContext, userId);
            }
        }
    }
}
