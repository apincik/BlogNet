using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Web.Extensions;

namespace Web.Filters
{
    public class CheckProjectSetAttribute : TypeFilterAttribute
{
        public CheckProjectSetAttribute() : base(typeof(CheckProjectSetFilter))
        {
        }
}

    public class CheckProjectSetFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int? projectId = context.HttpContext.Request.Cookies.GetProjectId();
            if (projectId == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                            { "action", "Index" }
                    }
                ).WithWarning("Info:", "Select project first.");
            }
        }
    }
}
