using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Blognet.Cms.WebAdmin.Extensions
{
    public static class AlertExtension
    {
        public static IActionResult WithSuccess(this IActionResult result, string title, string body)
        {
            return Alert(result, "success", title, body);
        }

        public static IActionResult WithInfo(this IActionResult result, string title, string body)
        {
            return Alert(result, "info", title, body);
        }

        public static IActionResult WithWarning(this IActionResult result, string title, string body)
        {
            return Alert(result, "warning", title, body);
        }

        public static IActionResult WithDanger(this IActionResult result, string title, string body)
        {
            return Alert(result, "danger", title, body);
        }

        private static IActionResult Alert(IActionResult result, string type, string title, string body)
        {
            return new AlertDecoratorResult(result, type, title, body);
        }
    }

    public class AlertDecoratorResult : IActionResult
    {
        public IActionResult Result { get; }
        public string Type { get; }
        public string Title { get; }
        public string Body { get; }

        public AlertDecoratorResult(IActionResult result, string type, string title, string body)
        {
            Result = result;
            Type = type;
            Title = title;
            Body = body;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var factory = context.HttpContext.RequestServices.GetService<ITempDataDictionaryFactory>();

            var tempData = factory.GetTempData(context.HttpContext);
            tempData["_alert.type"] = Type;
            tempData["_alert.title"] = Title;
            tempData["_alert.body"] = Body;

            await Result.ExecuteResultAsync(context);
        }
    }

}
