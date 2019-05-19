using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blognet.Cms.WebAdmin.Extensions
{
    public static class SessionExtension
    {
        public static void SetProjectId(this ISession session, params object[] args)
        {
            session.SetInt32("ProjectId", (int)args[0]);
        }

        public static int? GetProjectId(this ISession session)
        {
            return session.GetInt32("ProjectId");
        }
    }
}
