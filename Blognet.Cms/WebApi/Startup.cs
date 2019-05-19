using System;
using AutoMapper;
using Blognet.Cms.Core.Interfaces;
using Blognet.Cms.Infrastructure.Data;
using Blognet.Cms.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Blognet.Cms.WebApi.Filters;
using Blognet.Cms.Core.MappingProfiles;
using System.Reflection;
using Blognet.Cms.Core.Articles.Queries.GetWebCategoryArticles;
using Newtonsoft.Json;

namespace Blognet.Cms.WebApi
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

            services.AddDbContext<WebContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // App services
            services.AddScoped<ITime, Time>();

            // WebContext
            services.AddScoped<IWebContext, WebContext>();

            // Setup for AutoMapper.
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            Mapper.Initialize(cfg =>
                cfg.AddMaps(new[] {
                    typeof(ArticleMappingProfile)
                })
            );

            // MediatR
            //services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(GetWebCategoryArticlesQuery).GetTypeInfo().Assembly, typeof(Startup).GetTypeInfo().Assembly);

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
                // Allow reference loop in result objects, DTOs
                //.AddJsonOptions(options => {
                //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //});

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
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseStaticFiles();
                
                // Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMS API v1");
                });
            }

            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseMvc();
        }
    }
}
