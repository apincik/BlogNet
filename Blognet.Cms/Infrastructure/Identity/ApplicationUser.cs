using Blognet.Cms.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace Blognet.Cms.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public string GetEmail()
        {
            return Email;
        }

        public string GetUserName()
        {
            return UserName;
        }

        public Guid GetGuid()
        {
            return new Guid(Id);
        }
    }
}
