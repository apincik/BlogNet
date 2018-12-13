using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Identity;
using System;
using Microsoft.AspNetCore.Identity;
using Core.Interfaces;
using Core.Extensions;
using Infrastructure.Services;
using Core.Services;
using Core.Entities;
using AutoMapper;
using Web.Mappings;
using Web.Filters;

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

            #region DB_Identity

            services.AddDbContextPool<AppIdentityContext>(
                options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                mysqlOptions =>
                {
                    mysqlOptions
                        .ServerVersion(new Version(5, 7, 21), ServerType.MySql)
                        .CharSetBehavior(CharSetBehavior.AppendToAllColumns)
                        .AnsiCharSet(CharSet.Latin1)
                        .UnicodeCharSet(CharSet.Utf8mb4);

                }
            ));

            services.AddDbContext<WebContext>(
                options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                mysqlOptions =>
                {
                    mysqlOptions
                        .ServerVersion(new Version(5, 7, 21), ServerType.MySql)
                        .CharSetBehavior(CharSetBehavior.AppendToAllColumns)
                        .AnsiCharSet(CharSet.Latin1)
                        .UnicodeCharSet(CharSet.Utf8mb4);
                    
                }
            ));

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

            //Setup for AutoMapper.
            services.AddAutoMapper();
            Mapper.Initialize(config => config.AddProfile<MappingProfile>());

            //Application user services.
            services.AddTransient<UserManager<ApplicationUser>>();
            services.AddTransient<SignInManager<ApplicationUser>>();
            services.AddTransient<RoleManager<IdentityRole>>();

            //Custom services.
            services.AddScoped<ITime, Time>();

            //Domain services
            services.AddTransient(typeof(IAsyncModel<>), typeof(Infrastructure.Data.Repository<>));
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
            services.AddScoped<ISeoService, SeoService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IArticleSettingsService, ArticleSettingsService>();
            services.AddScoped<ITemplateVariableService, TemplateVariableService>();
            services.AddScoped<IPageForwardService, PageForwardService>();
            services.AddScoped<IMenuItemService, MenuItemService>();

            services.AddScoped<IImageFileUploadService, ImageFileUploadService>();

            //services.AddScoped<RedirectWhenNoProjectSetAttribute>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1).
                AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            // else
            // {
            //     app.UseExceptionHandler("/Home/Error");
            // }

            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();

            //Possible move to background task, anyway used only in dev env.
            if (env.IsDevelopment())
            {
                //Set DB default data, session.
                //app.SeedData().GetAwaiter().GetResult();
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
