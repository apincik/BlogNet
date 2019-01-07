using System;
using AutoMapper;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Services;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Identity;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;
using WebApi.Filters;
using WebApi.Mappings;

namespace WebApi
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

            // Setup for AutoMapper.
            services.AddAutoMapper();
            Mapper.Initialize(config => config.AddProfile<MappingProfile>());

            // App services
            services.AddScoped<ITime, Time>();
            services.AddScoped<IArticleRepository, ArticleRepository>();

            // MediatR
            services.AddMediatR(typeof(Startup));

            // @TODO UPDATE/REMOVE
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1).
                AddSessionStateTempDataProvider();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "CMS API", Version = "v1" });
                c.OperationFilter<ProjectHeaderOperationFilter>();
            });
        }

        // @TODO Update dev env setup
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseStaticFiles();
                
                // Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMS API v1");
                });

                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseMvc();
        }
    }
}
