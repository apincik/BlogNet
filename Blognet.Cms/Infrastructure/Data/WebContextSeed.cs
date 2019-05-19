using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blognet.Cms.Infrastructure.Data
{
    public static class WebContextSeed
    {
        public static async Task<Project> SeedAsync(WebContext webContext, string userId)
        {
            //Add project
            var exProject = await webContext.Set<Project>().FindAsync(1);
            if (exProject == null)
            {
                Project project = new Project();
                project.Id = 1;
                project.Name = "TestProject";
                project.DomainName = "localhost";
                project.UserId = userId;
                webContext.Add(project);
                await webContext.SaveChangesAsync();
                exProject = project;
            }

            //Add category
            var exCategory = await webContext.Set<Category>().FindAsync(1);
            if (exCategory == null)
            {
                Category category = new Category();
                category.Id = 1;
                category.Title = "Programming";
                category.ProjectId = 1;
                category.Status = Status.Active;
                webContext.Add(category);
                await webContext.SaveChangesAsync();
            }

            return exProject;
        }
    }
}
