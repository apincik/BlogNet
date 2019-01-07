using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Filters
{
    /// <summary>
    /// Add document header option for Swagger.
    /// </summary>
    public class ProjectHeaderOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();
            
            var domainParam = operation.Parameters.Where(p => p.Name == "projectDomain" && p.In == "header").SingleOrDefault();
            if(domainParam != null)
            {
                operation.Parameters.Remove(domainParam);
            }

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "projectDomain",
                In = "header",
                Type = "string",
                Required = true // set to false if this is optional
            });
        }
    }
}
