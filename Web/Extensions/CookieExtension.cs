using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Extensions
{
    public static class CookieExtension
    {
        public static void SetProjectId(this IResponseCookies cookies, int? projectId)
        {
            cookies.Append(
                "ProjectId", 
                projectId.ToString(), 
                new CookieOptions()
                {
                    Path = "/"
                }
            );
        }

        public static int? GetProjectId(this IRequestCookieCollection cookies)
        {
            if(cookies["ProjectId"] == null)
            {
                return null;
            } else
            {
                int tempVal;
                int? val = Int32.TryParse(cookies["ProjectId"], out tempVal) ? Int32.Parse(cookies["ProjectId"]) : (int?) null;
                return val;
            }
        }
    }
}
