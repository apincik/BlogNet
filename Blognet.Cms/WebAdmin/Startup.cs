using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Blognet.Cms.Infrastructure.Data;
using Blognet.Cms.Infrastructure.Identity;
using System;
using Microsoft.AspNetCore.Identity;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Infrastructure.Services;
using Blognet.Cms.Core.Services;
using AutoMapper;
using MediatR;
using System.Reflection;
using Blognet.Cms.Core.MappingProfiles;
using Blognet.Cms.Core.Albums.Queries.GetAlbumsList;
using Blognet.Cms.Infrastructure;
using FluentValidation.AspNetCore;

namespace Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
            });

            #region DB_Contexts_with_Identity

            services.AddDbContext<AppIdentityContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<WebContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityContext>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
            });

            #endregion

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Account/User/Login";
                options.LogoutPath = "/Account/User/Logout";
            });

            // WebContext
            services.AddScoped<IWebContext, WebContext>();

            // Setup for AutoMapper.
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            Mapper.Initialize(cfg =>
                cfg.AddMaps(new[] {
                    typeof(ArticleMappingProfile)
                })
            );

            // Application user services.
            services.AddTransient<UserManager<ApplicationUser>>();
            services.AddTransient<SignInManager<ApplicationUser>>();
            services.AddTransient<RoleManager<IdentityRole>>();

            // Custom services.
            services.AddScoped<ITime, Time>();

            // Domain services
            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();

            services.AddScoped<IImageFileUploadService, ImageFileUploadService>();
            services.AddScoped<IArticleAlbumService, ArticleAlbumService>();
            services.AddScoped<IArticleProcessorService, ArticleProcessorService>();

            //services.AddScoped<RedirectWhenNoProjectSetAttribute>();

            // MediatR
            services.AddMediatR(typeof(GetAlbumsListQueryHandler).GetTypeInfo().Assembly, typeof(Startup).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddSessionStateTempDataProvider()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(GetAlbumsListQueryHandler).GetTypeInfo().Assembly));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();

            if(env.IsDevelopment() || env.IsStaging())
            {
                // Set DB default data, session.
                // @review uncomment for data seeding...
                //app.SeedData().GetAwaiter().GetResult();
            }
            // Possible move to background task, anyway used only in dev env.
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=default}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
